using System;
using System.Collections;
using System.Collections.Generic;
using Messages.Extentions;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Notification : MonoBehaviour
{
    [SerializeField]
    private  Vector2 animationTarget = Vector2.zero;

    [SerializeField] 
    private float animationSpeed = 1.0f;

    [SerializeField] 
    private float notificationDuration;
    
    private RectTransform rectTransform;

    private Vector3 targetInPosition;
    private Vector3 targetOutPossition;
    private Vector3 transitionVector; 
    
    private enum AnimationState
    {
        Idle,
        In,
        Out
    }

    private AnimationState animatingState;
    private AnimationState currentVisibleState;
    private float tick;

    private float durationTick;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        SetStartingPosition();
    }

    private void SetStartingPosition()
    {
        targetOutPossition.y = rectTransform.sizeDelta.y + 50f;
        
        rectTransform.anchoredPosition = targetOutPossition;
        
        animatingState = AnimationState.Idle;
        currentVisibleState = AnimationState.Out;
    }

    public void TriggerNotification()
    {
        SetState(AnimationState.In);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TriggerNotification();
        }
        
        switch (animatingState)
        {
            case AnimationState.Idle:
                if (currentVisibleState == AnimationState.Out)
                {
                    return;
                }

                if (durationTick >= notificationDuration)
                {
                    //Play the notification out
                    SetState(AnimationState.Out);
                }
                break;
            case AnimationState.In:
                var moveIn = GetEased(targetOutPossition, targetInPosition, tick);
                rectTransform.anchoredPosition = moveIn;
                break;
            case AnimationState.Out:
                var moveOut = GetEased(targetInPosition, targetOutPossition, tick);
                rectTransform.anchoredPosition = moveOut;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        tick += Time.deltaTime * animationSpeed;
        
        durationTick += Time.deltaTime;

        if (tick >= 1.0f)
        {
            FinalizePosition();
        }
    }

    private void FinalizePosition()
    {
        switch (animatingState)
        {
            case AnimationState.Idle:
                break;
            case AnimationState.In:
                rectTransform.anchoredPosition = targetInPosition;
                currentVisibleState = AnimationState.In;
                break;
            case AnimationState.Out:
                rectTransform.anchoredPosition = targetOutPossition;
                currentVisibleState = AnimationState.Out;
                durationTick = 0.0f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        SetState(AnimationState.Idle);
    }

    private Vector3 GetEased(Vector3 from, Vector3 to, float time)
    {
        transitionVector.x = EasingFunction.EaseInCubic(from.x, to.x, time);
        transitionVector.y = EasingFunction.EaseInCubic(from.y, to.y, time);
        transitionVector.z = EasingFunction.EaseInCubic(from.z, to.z, time);

        return transitionVector;
    }

    private void SetState(AnimationState state)
    {
        if (animatingState == state || currentVisibleState == state)
        {
            return;
        }

        animatingState = state;

        tick = 0.0f;
    }
}
