using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("OnREfresh layoyut");
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        verticalLayoutGroup.SetLayoutVertical();
    }
}
