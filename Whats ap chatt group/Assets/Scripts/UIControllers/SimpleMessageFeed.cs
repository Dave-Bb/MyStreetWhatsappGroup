using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMessageFeed : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject spacer;
    [SerializeField] private float spacing;
    [SerializeField] private float travelSpace;
    [SerializeField] private VerticalLayoutGroup layoutGroup;

    private float cachedHeight;

    private bool dirty;

    private float tick;
    public float lerpSpeed = 1f;

    private bool isLerping;

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
                //position.y += newMessage.GetComponent<RectTransform>().sizeDelta.y + travelSpace;
                position.y = Mathf.Lerp(0, travelSpace, tick);
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
            //position.y += newMessage.GetComponent<RectTransform>().sizeDelta.y + travelSpace;
            position.y = 0;
            contentRect.position = position;
            dirty = false;
        }
    }

    private void AddItem()
    {
        Debug.Break();
        var newMessage = Instantiate(itemPrefab, contentRect);
       // Instantiate(spacer, contentRect);
        var currentRect = contentRect.sizeDelta;
        var newMessageSize = newMessage.GetComponent<RectTransform>().sizeDelta.y + spacing;
        currentRect.y += newMessageSize;
        contentRect.sizeDelta = currentRect;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        var position = contentRect.position;
        //position.y += newMessage.GetComponent<RectTransform>().sizeDelta.y + travelSpace;
        /*position.y = 0f;
        contentRect.position = position;*/
        dirty = true;
        cachedHeight = newMessageSize - spacing;
        tick = 0.0f;
        isLerping = true;
        // Debug.Break();
    }
}
