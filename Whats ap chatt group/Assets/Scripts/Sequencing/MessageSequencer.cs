﻿using System.Collections.Generic;
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
        private void Awake()
        {
            LoadMessages();
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
            //  throw new NotImplementedException();
            if (!trackController.TargetAudioSource.isPlaying)
            {
                return;
            }

            if (messageQueue.Count <= 0)
            {
                return;
            }
            
            var nextMessage = messageQueue.Peek();
            if (trackController.CurrentTime >= nextMessage.sequence.StageTime)
            {
                messageDisplayer.DisplayMessage(messageQueue.Dequeue());
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
            messageQueue = new Queue<Message>();
            foreach (var message in messages)
            {
                messageQueue.Enqueue(message);
            }
            
            
        }
    }
}