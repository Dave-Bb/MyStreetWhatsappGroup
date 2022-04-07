using System;
using System.Collections.Generic;
using System.Linq;
using Messages;
using MessagesUI;
using UnityEngine;

namespace Sequencing
{
    /// <summary>
    /// Load all the message types, sort them into an order
    /// then play them off when its their turn following the <see cref="trackController"/>
    /// </summary>
    public class MessageSequencer : MonoBehaviour
    {
        [SerializeField] 
        private SimpleTrackController trackController;

        [SerializeField] 
        private MessagePreview messageDisplayer;

        [SerializeField] 
        private string messagePath = "Messages/TextMessages";

        [SerializeField] 
        private Message currentMessage;

        private IEnumerable<Message> messages;

        private Queue<Message> messageQueue;

        public IEnumerable<Message> Messages => messages;

        public Action<Message> NewMessageBorn;
        
        private void Awake()
        {
            LoadMessages();
            ResetAllMessageStates();

            if (trackController != null)
            {
                trackController.Stopped += OnStopped;
            }
        }

        private void OnStopped()
        {
            BuildQueue();
            ResetAllMessageStates();
        }

        private void ResetAllMessageStates()
        {
            foreach (var message in messages)
            {
                message.sequence.State = MessageState.ReadyToPlay;
            }
        }

        private void Update()
        {
            if (!trackController.TargetAudioSource.isPlaying)
            {
                return;
            }

            if (messageQueue == null || messageQueue.Count <= 0)
            {
                return;
            }
            
            var nextMessage = messageQueue.Peek();
            if (trackController.CurrentTime >= nextMessage.sequence.StageTime)
            {
                var newMessage = messageQueue.Dequeue();
                messageDisplayer.DisplayMessage(newMessage);
                NewMessageBorn?.Invoke(newMessage);
            }

        }

        public void SetCurrentMessageAtTime()
        {
            currentMessage.sequence.StageTime = trackController.CurrentTime;
            currentMessage.sequence.State = MessageState.ReadyToPlay;
        }

        private void LoadMessages()
        {
            messages = Resources.LoadAll(messagePath, typeof(Message)).Cast<Message>();
            messages = messages.OrderBy(x => x.sequence.StageTime);
            BuildQueue();
        }

        private void BuildQueue()
        {
            messageQueue = new Queue<Message>();
            foreach (var message in messages)
            {
                messageQueue.Enqueue(message);
            }
        }

        private void OnDestroy()
        {
            if (trackController != null)
            {
                trackController.Stopped -= OnStopped;
            }
        }
    }
}