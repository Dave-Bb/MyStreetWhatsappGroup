using System;
using Messages;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sequencing
{
    [RequireComponent(
        typeof(RectTransform), 
        typeof(Image))]
    public class MarkerPoint : MonoBehaviour, IPointerClickHandler
    {
        private RectTransform rectTransform;
        private Image image;

        private Message markerMessage;

        public Action<Message> PointerClicked;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        public void SetMarker(float position, Message message)
        {
            markerMessage = message;
            SetXPoint(position);
            SetColor();
        }
        
        private void SetXPoint(float xPoint)
        {
            rectTransform.anchoredPosition = new Vector2(xPoint, rectTransform.anchoredPosition.y);
        }
        
        private void SetColor()
        {
            switch (markerMessage.messageType)
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
                    throw new ArgumentOutOfRangeException(nameof(markerMessage.messageType), markerMessage.messageType, null);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClicked?.Invoke(markerMessage);
        }
    }
}