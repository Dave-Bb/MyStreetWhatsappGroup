using System;
using Messages.UIControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    public class SetImageMessage : SetMessageWithSize
    {
        [SerializeField] 
        private ImageMessage message;
        
        [SerializeField] 
        private Image image;
        
        [SerializeField] 
        private TextMeshProUGUI nameNumber;
    
        [SerializeField] 
        private TextMeshProUGUI messageBody;
    
        [SerializeField] 
        private TextMeshProUGUI time;

       
        
        private void Awake()
        {
            if (message != null)
            {
                SetContent(message);
            }
        }

        public void SetContent(ImageMessage imageMessage)
        {
            //Set name and colour
            if (nameNumber != null)
            {
                nameNumber.text = imageMessage.Message.contact.NameNumber;
                nameNumber.color = imageMessage.Message.contact.Color;
            }


            if (message != null)
            {
                messageBody.text = imageMessage.Message.MessageBody;
            }

            if (time != null)
            {
                time.text = imageMessage.Message.Time;
            }

            if (image != null)
            {
                image.sprite = imageMessage.Image;
            }
            
            MessageSet?.Invoke(imageMessage);
        }
    }
}