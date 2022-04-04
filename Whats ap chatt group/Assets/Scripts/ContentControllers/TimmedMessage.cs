using System;
using Messages;
using UnityEngine.Serialization;

namespace ContentControllers
{
    [Serializable]
    public class TimmedMessage
    {
        [FormerlySerializedAs("Message")] public TextMessage textMessage;
        public float StageTime;
    }
}