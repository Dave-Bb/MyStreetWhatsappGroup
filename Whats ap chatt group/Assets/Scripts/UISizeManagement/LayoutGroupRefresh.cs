using System;
using UnityEngine;
using UnityEngine.UI;

namespace UISizeManagement
{
    public class LayoutGroupRefresh : MonoBehaviour
    {
        private VerticalLayoutGroup verticalLayoutGroup;
        private RectTransform rectTransform;

        public Action doRefresh;

        private void Awake()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnRefreshLayoutGroup()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            verticalLayoutGroup.SetLayoutVertical();
        }
    }
}
