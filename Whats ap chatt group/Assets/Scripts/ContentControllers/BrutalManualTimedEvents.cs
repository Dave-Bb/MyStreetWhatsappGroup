using System;
using System.Collections.Generic;
using Messages;
using UnityEngine;

namespace ContentControllers
{
    public class BrutalManualTimedEvents : MonoBehaviour
    {
        [SerializeField]
        public List<TimmedMessage> Messages = new List<TimmedMessage>();

        private Queue<TimmedMessage> messageQueue = new Queue<TimmedMessage>();

        public float startDelay = 1.0f;
        
        private float stageTime;

        public Action<TextMessage> PushNewMessage;

        private void Awake()
        {
            RebuildAndRestart();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("RESET");
                RebuildAndRestart();
            }
            

            /*if (stageTime < startDelay)
            {
                stageTime += Time.deltaTime;
                return;
            }*/
            
            //Check the next item in the queue, if the current time is higher, then play that shit! 
            if (messageQueue.Count != 0)
            {
                var nextInLine = messageQueue.Peek();
                if (stageTime >= nextInLine.StageTime)
                {
                    Play(messageQueue.Dequeue());
                    return;
                }
            }
          
            
            

            stageTime += Time.deltaTime;
        }

        private void RebuildAndRestart()
        {
            

            messageQueue.Clear();
            foreach (var timmedMessage in Messages)
            {
                messageQueue.Enqueue(timmedMessage);
            }
            
            stageTime = 0.0f;
        }

        private void Play(TimmedMessage message)
        {
            Debug.Log(message.textMessage.MessageBody);
            PushNewMessage?.Invoke(message.textMessage);
        }
    }
}