using Messages;
using UnityEngine;

namespace ContentControllers
{
    public class AddItemFromAnimation : MonoBehaviour
    {
        public void PrintMessage(TextMessage textMessage)
        {
            Debug.Log(textMessage.MessageBody);
        }
    }
}