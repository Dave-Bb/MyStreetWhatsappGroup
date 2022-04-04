using Messages;
using TMPro;
using UnityEngine;

namespace MessagesUI
{
    /// <summary>
    /// Most basic message, <see cref="SetGifMessage"/> and <see cref="SetImageMessage"/> are children of this.
    /// </summary>
    public class SetMessage : SetMessageWithSize
    {
        [SerializeField] 
        private TextMeshProUGUI nameNumber;

        [SerializeField] 
        private TextMeshProUGUI messageBody;

        [SerializeField] 
        private TextMeshProUGUI time;

        [SerializeField] 
        protected Message message;

        private void Start()
        {
            if (message != null)
            {
                SetContent(message);
            }
        }

        public virtual void SetContent(Message textMessage)
        {
            nameNumber.text = textMessage.contact.NameNumber;
            nameNumber.color = textMessage.contact.Color;

            messageBody.text = textMessage.MessageBody;

            time.text = textMessage.Time;
        }
    }
}
