using Messages;
using Messages.Extentions;
using MessagesUI;
using UnityEngine;
using UnityEngine.UI;

namespace ContentControllers
{
    /// <summary>
    /// Most basic ass message feed 
    /// </summary>
    public class SimpleMessageFeed : MonoBehaviour
    {
        [SerializeField]
        private RectTransform contentRect;

        [SerializeField] 
        private GameObject itemPrefab;

        [SerializeField] 
        private float spacing;

        [SerializeField] 
        private float travelSpace;

        [SerializeField] 
        private VerticalLayoutGroup layoutGroup;

        [SerializeField] 
        private BrutalManualTimedEvents timedMessageEvents;

        private bool dirty;

        private float tick;
        public float lerpSpeed = 1f;

        private bool isLerping;

        private void Awake()
        {
            timedMessageEvents.PushNewMessage += OnPushNewMessage;
        }

        private void OnPushNewMessage(TextMessage textMessage)
        {
            AddMessage(textMessage);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddItem();
            }

            if (isLerping)
            {
                if (tick < 1)
                {
                    var position = contentRect.position;
                    position.y = EasingFunction.EaseInQuad(0, travelSpace, tick);
                    contentRect.position = position;
                    tick += Time.deltaTime * lerpSpeed;
                }
                else
                {
                    isLerping = false;
                    layoutGroup.SetLayoutVertical();
                    LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
                }
            }



            if (dirty)
            {
                var position = contentRect.position;
                position.y = 0;
                contentRect.position = position;
                dirty = false;
            }
        }

        public void AddMessage(TextMessage textMessage)
        {
            var newMessage = Instantiate(itemPrefab, contentRect);
            var messageManager = newMessage.GetComponent<SetMessage>();
            messageManager.SetContent(textMessage);
            var currentRect = contentRect.sizeDelta;
            var newMessageSize = newMessage.GetComponent<RectTransform>().sizeDelta.y + spacing;
            currentRect.y += newMessageSize * 2;
            contentRect.sizeDelta = currentRect;
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
            dirty = true;
            tick = 0.0f;
            isLerping = true;
        }

        private void AddItem()
        {
            var newMessage = Instantiate(itemPrefab, contentRect);
            var currentRect = contentRect.sizeDelta;
            var newMessageSize = newMessage.GetComponent<RectTransform>().sizeDelta.y + spacing;
            currentRect.y += newMessageSize;
            contentRect.sizeDelta = currentRect;
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
            var position = contentRect.position;
            dirty = true;
            tick = 0.0f;
            isLerping = true;
        }
    }
}
