using UnityEngine;
using UnityEngine.Serialization;

namespace Messages
{
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Messages/TextMessage", order = 1)]
    public class Message : ScriptableObject
    {
        public Contact contact;
        public string MessageBody;
        public string Time;
    }
}