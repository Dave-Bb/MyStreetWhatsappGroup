﻿using System;
using Messages;
using UnityEngine;
using UnityEngine.UI;

namespace MessagesUI
{
    public class MessagePreview : MonoBehaviour
    {
        [SerializeField] private GameObject textMessagePrefab;
        [SerializeField] private GameObject imageMessagePrefab;
        [SerializeField] private GameObject gifMessagePrefab;

        [SerializeField] private RectTransform previewAncorPoint;

        [SerializeField] private Message message;

        [Header("UI")] [SerializeField] private Button refreshButton;


        private GameObject activePreviewMessage;

        private void Awake()
        {
            if (refreshButton != null)
            {
                refreshButton.onClick.AddListener(RefreshWithMessage);
            }
        }

        private void RefreshWithMessage()
        {
            if (message != null)
            {
                SetNewMessage(message);
            }
        }

        public void SetNewMessage(Message newMessage)
        {
            if (activePreviewMessage != null)
            {
                Destroy(activePreviewMessage);
                activePreviewMessage = null;
            }

            switch (newMessage.messageType)
            {
                case MessageType.Text:
                    if (newMessage is Message textMessage)
                    {
                        activePreviewMessage = Instantiate(textMessagePrefab, previewAncorPoint);
                        activePreviewMessage.GetComponent<SetMessage>().SetContent(textMessage);
                    }
                    break;
                case MessageType.Image:
                    if (newMessage is ImageMessage imageMessage)
                    {
                        activePreviewMessage = Instantiate(imageMessagePrefab, previewAncorPoint);
                        activePreviewMessage.GetComponent<SetImageMessage>().SetContent(imageMessage);
                    }
                    break;
                case MessageType.Gif:
                    if (newMessage is GifMessage gifMessage)
                    {
                        activePreviewMessage = Instantiate(gifMessagePrefab, previewAncorPoint);
                        activePreviewMessage.GetComponent<SetGifMessage>().SetContent(gifMessage);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            

            

            
        }
    }
}