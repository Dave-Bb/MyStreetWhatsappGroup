using Messages;
using UnityEngine;
using UnityEngine.UI;

namespace MessagesUI
{
    /// <summary>
    /// Sets the image values for an image message. Should get a <see cref="ImageMessage"/>
    /// </summary>
    public class SetImageMessage : SetMessage
    {
        [SerializeField] 
        private Image image;
        
        public override void SetContent(Message textMessage)
        {
            base.SetContent(textMessage);

            if (textMessage is ImageMessage imageMessage)
            {
                if (image != null)
                {
                    image.sprite = imageMessage.Image;
                }
            
                MessageSet?.Invoke(imageMessage);
            }
        }
        
    }
}