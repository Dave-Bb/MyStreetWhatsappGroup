using Messages;
using MessagesUI;
using UISizeManagement.SizeReporters;
using UnityEngine;

namespace UISizeManagement
{
    /// <summary>
    /// If you put an image here, it will wait till the message got set, then resize its self.
    /// Attached to this game object should also then be a <see cref="RectSizeReporter"/>
    /// which will fire events when the size changed to a scale follwer 
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ImageSizeFollower : MonoBehaviour
    {
        [SerializeField] 
        private SetMessageWithSize setImage;
        
        private RectTransform rectTransform;

        private IImage currentImageMessage;
        
        private void Awake()
        {
            if (setImage != null)
            {
                setImage.MessageSet += OnMessageSet;
            }

            rectTransform = GetComponent<RectTransform>();
        }

        private void OnMessageSet(IImage newImageMessage)
        {
            currentImageMessage = newImageMessage;
            SetRootRectSize(currentImageMessage.GetImageSize());
        }

        private void SetRootRectSize(Vector2 imageSize)
        {
            rectTransform.sizeDelta = imageSize;
        }
    }
}