using System;
using System.Collections;
using System.Collections.Generic;
using Messages.UIControllers;
using UnityEngine;

public class ScaleFollower : MonoBehaviour
{
    [SerializeField] private TextBodySizeReporter textBodySizeReporter;
    [SerializeField] private Vector2 minimumDelta;
    [SerializeField] private float heightSizeBuffer = 120.0f;
    [SerializeField] private float widthSizeBuffer = 120.0f;
    [SerializeField] private ScaleFollowType followType;

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
        
        Debug.Log($"Update size {newDelta}");
        
        rectTransform.sizeDelta = new Vector2(newDelta.x , newDelta.y);
        
        Canvas.ForceUpdateCanvases();
        
        //gameObject.SendMessageUpwards("OnRefreshLayoutGroup");
        //ectTransform.rect.Set(thisRect.x, thisRect.y, thisRect.width, height + sizeBuffer);
    }
}
