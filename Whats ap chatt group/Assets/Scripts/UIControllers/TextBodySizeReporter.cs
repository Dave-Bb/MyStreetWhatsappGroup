using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBodySizeReporter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBodyText;

    private Vector2 currentDizeDelta;

    public Action<Vector2> SizeChanged;

    private bool didForceUpdate;

    private int awakeOnFrame;
    
    public void Awake()
    {
        awakeOnFrame = Time.frameCount;
    }

    private void Update()
    {
        if (!didForceUpdate && awakeOnFrame >= Time.frameCount + 1)
        {
            SizeChanged?.Invoke(messageBodyText.GetRenderedValues(true));
            didForceUpdate = true;
        }
        
        var currentValue = messageBodyText.GetRenderedValues(true);
        
        if (Math.Abs(currentValue.y - currentDizeDelta.y) > Mathf.Epsilon || Math.Abs(currentValue.x - currentDizeDelta.x) > Mathf.Epsilon)
        {
            
            if (currentValue.y == 0 || currentValue.x == 0)
            {
                return;
            }

            currentDizeDelta = currentValue;
            SizeChanged?.Invoke(currentDizeDelta);
            Debug.Log($"Current Value Changed{currentDizeDelta}");
        }
        
    }

    public void ForceUpdate()
    {
        
    }
}
