using System;
using System.Collections.Generic;
using Messages;
using Messages.Extentions;
using MessagesUI;
using Sequencing;
using UnityEngine;
using UnityEngine.UI;

namespace ContentControllers
{
    /// <summary>
    /// Most basic ass message feed 
    /// </summary>
    public class SimpleMessageFeed : MonoBehaviour
    {
        [Header("Message Prefabs")]
        [SerializeField] 
        private GameObject textMessagePrefab;
       
        [SerializeField] 
        private GameObject imageMessagePrefab;
        
        [SerializeField] 
        private GameObject gifMessagePrefab;
        
        [SerializeField] 
        private Message testMessage;
        
        [SerializeField]
        private RectTransform contentRect;

        [SerializeField] 
        private float spacing;

        [SerializeField] 
        private float travelSpace;

        [SerializeField] 
        private VerticalLayoutGroup layoutGroup;

        [SerializeField] private MessageSequencer sequencer;

        private bool dirty;

        private float tick;
        public float lerpSpeed = 1f;

        private bool isLerping;

        private Queue<GameObject> visibleMessageQueue;

        private int maxVisibleMessages = 10;

        private GameObject activePreviewMessage;

        private void Awake()
        {
            visibleMessageQueue = new Queue<GameObject>(10);

            if (sequencer != null)
            {
                sequencer.NewMessageBorn += OnNewMessageBorn;
            }
        }

        private void OnNewMessageBorn(Message message)
        {
            AddMessage(message);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddMessage(testMessage);
            }

            if (isLerping)
            {
                if (tick < 1)
                {
                    var position = contentRect.position;
                    position.y = EasingFunction.EaseInQuad(0, travelSpace, tick);
                    contentRect.position = position;
                    tick += Time.deltaTime * lerpSpeed;
                }
                else
                {
                    isLerping = false;
                    layoutGroup.SetLayoutVertical();
                    LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
                }
            }
        }

        private void LateUpdate()
        {
            if (contentRect.anchoredPosition.y != 0)
            {
                var position = contentRect.anchoredPosition;
                position.y = 0;
                contentRect.anchoredPosition = position;
                dirty = false;
            }
        }

        public void AddMessage(Message testMessage)
        {
            var newMessage = GetNewMessage(testMessage, contentRect.transform);
            if (visibleMessageQueue.Count == maxVisibleMessages)
            {
                Destroy(visibleMessageQueue.Dequeue());
            }
            visibleMessageQueue.Enqueue(newMessage);
            
            var currentRect = contentRect.sizeDelta;
            var newMessageSize = newMessage.GetComponent<RectTransform>().sizeDelta.y + spacing;
            currentRect.y += newMessageSize * 2;
            contentRect.sizeDelta = currentRect;
            dirty = true;
            tick = 0.0f;
            isLerping = true;
        }

        private GameObject GetNewMessage(Message newMessage, Transform parent)
        {
            switch (newMessage.messageType)
            {
                case MessageType.Text:
                    if (newMessage is Message textMessage)
                    {
                        activePreviewMessage = Instantiate(textMessagePrefab, parent);
                        activePreviewMessage.GetComponent<SetMessage>().SetContent(textMessage);
                    }
                    break;
                case MessageType.Image:
                    if (newMessage is ImageMessage imageMessage)
                    {
                        activePreviewMessage = Instantiate(imageMessagePrefab, parent);
                        activePreviewMessage.GetComponent<SetImageMessage>().SetContent(imageMessage);
                    }
                    break;
                case MessageType.Gif:
                    if (newMessage is GifMessage gifMessage)
                    {
                        activePreviewMessage = Instantiate(gifMessagePrefab, parent);
                        activePreviewMessage.GetComponent<SetGifMessage>().SetContent(gifMessage);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return activePreviewMessage;
        }
    }
}
