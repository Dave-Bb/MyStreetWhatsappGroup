using System;
using TMPro;
using UnityEngine;

namespace Messages.UIControllers.SizeReporters
{
    public class SizeReporter : MonoBehaviour
    {
       // [SerializeField] private TextMeshProUGUI messageBodyText;

        private Vector2 targetSizeDelta;

        private Vector2 currentDizeDelta;

        public Action<Vector2> SizeChanged;

        private bool didForceUpdate;

        private int awakeOnFrame;
    
        protected virtual void Awake()
        {
            awakeOnFrame = Time.frameCount;
        }

        protected virtual void Update()
        {
            if (!didForceUpdate && awakeOnFrame >= Time.frameCount + 1)
            {
                SizeChanged?.Invoke(GetSizeDelta());
                didForceUpdate = true;
            }
        
            var currentValue = GetSizeDelta();
        
            if (Math.Abs(currentValue.y - currentDizeDelta.y) > Mathf.Epsilon || Math.Abs(currentValue.x - currentDizeDelta.x) > Mathf.Epsilon)
            {
            
                if (currentValue.y == 0 || currentValue.x == 0)
                {
                    return;
                }

                currentDizeDelta = currentValue;
                SizeChanged?.Invoke(currentDizeDelta);
                Debug.Log($"Current Value Changed{currentDizeDelta}");
            }
        
        }

        protected virtual Vector2 GetSizeDelta()
        {
            return targetSizeDelta;
        }

    }
}