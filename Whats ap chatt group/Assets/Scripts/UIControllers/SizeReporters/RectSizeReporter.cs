using UnityEngine;

namespace Messages.UIControllers.SizeReporters
{
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