using System;
using Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Sequencing
{
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class MarkerPoint : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Image image;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        public void SetMarker(float position, MessageType messageType)
        {
            SetXPoint(position);
            SetColor(messageType);
        }
        
        private void SetXPoint(float xPoint)
        {
            rectTransform.anchoredPosition = new Vector2(xPoint, rectTransform.anchoredPosition.y);
        }
        
        private void SetColor(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Text:
                    image.color = Color.blue;
                    break;
                case MessageType.Image:
                    image.color = Color.yellow;
                    break;
                case MessageType.Gif:
                    image.color = Color.cyan;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null);
            }
        }
    }
}