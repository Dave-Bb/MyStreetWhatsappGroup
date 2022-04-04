using UnityEngine;

namespace Messages.UIControllers
{
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