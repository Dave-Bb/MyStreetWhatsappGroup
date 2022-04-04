using System;
using Messages;
using UnityEngine;

namespace MessagesUI
{
    /// <summary>
    /// This is the final base of the Message setting classes.
    /// This allows other components to listen for when the content got set, to then
    /// resize. Allowing for some padding and adjustments.  
    /// </summary>
    public class SetMessageWithSize : MonoBehaviour
    {
        public Action<IImage> MessageSet;
    }
}