using System;

namespace Sequencing
{
    [Serializable]
    public class SequencedMessage
    {
        public float StageTime;
        public MessageState State;
    }
}