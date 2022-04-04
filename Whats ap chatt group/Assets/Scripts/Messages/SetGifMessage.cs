using TMPro;
using UnityEngine;

namespace Messages
{
    public class SetGifMessage : SetMessageWithSize
    {
        [SerializeField] private GifPlayer gifPlayer;
        [SerializeField] 
        private GifMessage message;
        
        [SerializeField] 
        private TextMeshProUGUI nameNumber;
    
        [SerializeField] 
        private TextMeshProUGUI messageBody;
    
        [SerializeField] 
        private TextMeshProUGUI time;
        
        private void Start()
        {
            if (message != null)
            {
                SetContent(message);
            }
        }

        public void SetContent(GifMessage imageMessage)
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

            if (gifPlayer != null)
            {
                gifPlayer.SetGifFrames(imageMessage.Frames);
            }

            MessageSet?.Invoke(imageMessage);
        }
    }
}