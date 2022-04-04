using System;
using UISizeManagement;
using UISizeManagement.SizeReporters;
using UnityEngine;

namespace UnityEngineInternal
{
    /// <summary>
    /// Used with a <see cref="SizeReporter"/> to then update the scale of this rect along with that.
    /// These extra fields are used to provide padding and fine tuning depending what the content is.
    /// You could also only follow height or width or both. 
    /// </summary>
    public class ScaleFollower : MonoBehaviour
    {
        [SerializeField] 
        private SizeReporter textBodySizeReporter;
        
        [SerializeField] 
        private Vector2 minimumDelta;
        
        [SerializeField] 
        private float heightSizeBuffer = 120.0f;
        
        [SerializeField] 
        private float widthSizeBuffer = 120.0f;
        
        [SerializeField] 
        private ScaleFollowType followType;

        public bool forceSize;
        public float force;

        private RectTransform rectTransform;
        private Vector2 newDelta;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            textBodySizeReporter.SizeChanged += OnHeightChanged;
        }

        private void Update()
        {
            if (!forceSize)
            {
                return;

            }

            var currentSizeDelta = rectTransform.sizeDelta;
            currentSizeDelta.y = force;
            rectTransform.sizeDelta = currentSizeDelta;
        }


        private void OnHeightChanged(Vector2 sizeDelta)
        {
            var currentSizeDelta = rectTransform.sizeDelta;
            newDelta = sizeDelta;
            switch (followType)
            {
                case ScaleFollowType.WidthAndHeight:
                    break;
                case ScaleFollowType.Width:
                    newDelta.x = currentSizeDelta.x;
                    break;
                case ScaleFollowType.Height:
                    newDelta.y = currentSizeDelta.y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            newDelta.x += widthSizeBuffer;
            newDelta.y += heightSizeBuffer;

            if (newDelta.x < minimumDelta.x)
            {
                newDelta.x = minimumDelta.x;
            }

            if (newDelta.y < minimumDelta.y)
            {
                newDelta.y = minimumDelta.y;
            }

            rectTransform.sizeDelta = new Vector2(newDelta.x, newDelta.y);

            Canvas.ForceUpdateCanvases();
        }
    }
}
