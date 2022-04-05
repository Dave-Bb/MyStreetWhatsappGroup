using UnityEngine;

namespace Messages
{
    /// <summary>
    /// The base message, it will always have a contact, text component and time
    /// </summary>
    public class Message : ScriptableObject
    {
        public Contact contact;
        public string MessageBody;
        public string Time;
        public MessageType messageType;
    }
}