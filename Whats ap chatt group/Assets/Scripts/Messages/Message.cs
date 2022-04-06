using System;
using Sequencing;
using UnityEngine;

namespace Messages
{
    /// <summary>
    /// The base message, it will always have a contact, text component and time
    /// </summary>
    [Serializable]
    public class Message : ScriptableObject
    {
        public Contact contact;
        public string MessageBody;
        public string Time;
        public MessageType messageType;
        [SerializeField]
        public SequencedMessage sequence;
    }
}