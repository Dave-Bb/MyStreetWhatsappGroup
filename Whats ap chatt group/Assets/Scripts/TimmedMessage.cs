using System;

namespace Messages
{
    [Serializable]
    public class TimmedMessage
    {
        public Message Message;
        public float StageTime;
    }
}