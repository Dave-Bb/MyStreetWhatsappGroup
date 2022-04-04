using UnityEngine;

namespace UISizeManagement.SizeReporters
{
    /// <summary>
    /// Reports when the size of a rect changed.
    /// Used to manage the size of the items in the list and to scale the messages nice
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class RectSizeReporter : SizeReporter
    { 
        private RectTransform targetRect;

        protected override void Awake()
        {
            base.Awake();
            targetRect = GetComponent<RectTransform>();
        }

        protected override Vector2 GetSizeDelta()
        {
            return targetRect.sizeDelta;
        }
    }
}