using Messages;
using MessagesUI;
using UnityEngine;
using UnityEngine.UI;

namespace ContentControllers
{
    public class ScrollPaneItemAdder : MonoBehaviour
    {
        [SerializeField] 
        private GameObject itemPlacHolder;

        [SerializeField] 
        private RectTransform contentRect;

        [SerializeField] 
        private VerticalLayoutGroup layoutGroup;

        [SerializeField] 
        private ScrollRect scrollRect;

        public float scrollNormalized = 0f;

        //private float lerpTargetOffset = 240;
        private float lerpTarget;
        private float lerpOrigon;

        private float lerpTime;

        public float timeMultipler = 1f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddNewItem();
            }
            
            if (lerpTime <= 1)
            {
                lerpTime += Time.deltaTime * timeMultipler;
                contentRect.offsetMax = new Vector2(0, Mathf.Lerp(lerpOrigon, lerpTarget, lerpTime));
            }
        }

        public void NEWMEssage(TextMessage textMessage)
        {
            var newItem = Instantiate(itemPlacHolder, contentRect);
            var setMessage = newItem.GetComponent<SetMessage>();
            setMessage.SetContent(textMessage);
            var itemRect = itemPlacHolder.GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        private void AddNewItem()
        {
            var newItem = Instantiate(itemPlacHolder, contentRect);
            var itemRect = itemPlacHolder.GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
            // contentRect.sizeDelta = new Vector2(contentRect.rect.width, contentRect.rect.height + itemRect.rect.height + layoutGroup.spacing);

        }

        private void SetContentRect(GameObject newItem)
        {
            RectTransform itemRectTransform = newItem.GetComponent<RectTransform>();
            if (itemRectTransform == null)
            {
                return;
            }

            var rect = contentRect.rect;
            Debug.Log(itemRectTransform.rect.height + layoutGroup.spacing);
            rect.x += itemRectTransform.rect.x + layoutGroup.spacing;
            contentRect.rect.position.Set(contentRect.rect.position.x, contentRect.rect.position.y + 200);
            //  contentRect.rect
        }
    }
}
