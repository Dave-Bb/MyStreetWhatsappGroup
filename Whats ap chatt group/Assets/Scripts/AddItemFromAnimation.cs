using UnityEngine;

namespace Messages
{
    public class AddItemFromAnimation : MonoBehaviour
    {
        public void PrintMessage(Message message)
        {
            Debug.Log(message.MessageBody);
        }
    }
}