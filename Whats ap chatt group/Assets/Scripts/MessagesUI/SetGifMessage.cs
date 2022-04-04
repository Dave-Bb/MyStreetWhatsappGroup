using Messages;
using UnityEngine;

namespace MessagesUI
{
    /// <summary>
    /// Set the gif image. These are attached to the gif image prefab.
    /// When they get created, they should receive a <see cref="GifMessage"/>
    /// </summary>
    public class SetGifMessage : SetMessage
    {
        [SerializeField] 
        private GifPlayer gifPlayer;
        
        private void Start()
        {
            if (message != null)
            {
                SetContent(message);
            }
        }
        
        public override void SetContent(Message message)
        {
            base.SetContent(message);

            if (message is GifMessage imageMessage)
            {
                if (gifPlayer != null)
                {
                    gifPlayer.SetGifFrames(imageMessage.Frames);
                }

                MessageSet?.Invoke(imageMessage);
            }
        }
    }
}