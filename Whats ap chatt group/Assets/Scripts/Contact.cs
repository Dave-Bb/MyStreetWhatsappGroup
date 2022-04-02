using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Contact", order = 1)]
    public class Contact : ScriptableObject
    {
        public string NameNumber;
        public Color Color;
    }
}