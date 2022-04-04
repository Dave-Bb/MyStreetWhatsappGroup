using UnityEngine;

namespace Messages
{
    /// <summary>
    /// Contact, each message should also have one of theses. 
    /// </summary>
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Contact", order = 1)]
    public class Contact : ScriptableObject
    {
        public string NameNumber;
        public Color Color;
    }
}