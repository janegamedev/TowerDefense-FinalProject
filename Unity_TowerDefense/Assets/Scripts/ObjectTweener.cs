using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    MOVE,
    SCALE,
    SCALEX,
    SCALEY,
}

public class ObjectTweener : MonoBehaviour
{
    public AnimationType animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;

    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;

    public bool showOnEnable;
    public bool workOnDisable;
    
    private GameObject _objectToAnimate;
    private LTDescr _tweenObject;

    private void OnEnable()
    {
        if(!showOnEnable) return;

        Show();
    }

    private void Show()
    {
        HandleTween();
    }

    public void HandleTween()
    {
        if (_objectToAnimate == null)
        {
            _objectToAnimate = gameObject;
        }

        switch (animationType)
        {
            case AnimationType.MOVE:
                MoveAbsolute();
                break;
            
            case AnimationType.SCALE:
                Scale();
                break;
            
            case AnimationType.SCALEX:
                Scale();
                break;
            
            case AnimationType.SCALEY:
                Scale();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        _tweenObject.setDelay(delay);
        _tweenObject.setEase(easeType);

        if (loop)
        {
            _tweenObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            _tweenObject.setLoopPingPong();
        }
    }

    private void Scale()
    {
        if (startPositionOffset)
        {
            _objectToAnimate.transform.localScale = from;
        }

        _tweenObject = LeanTween.scale(_objectToAnimate, to, duration);
    }

    private void MoveAbsolute()
    {
        if (startPositionOffset)
        {
            _objectToAnimate.transform.localScale = from;
        }
        
        to += from;
        _tweenObject = LeanTween.move(_objectToAnimate, to, duration);
    }

    public void SwapDirection()
    {
        var temp = from;
        from = to;
        to = temp;
    }

    public void Disable()
    {
        SwapDirection();
        HandleTween();

        _tweenObject.setOnComplete(() =>
        {
            SwapDirection();
            gameObject.SetActive(false);
        });
    }

    public void Disable(Action onCompleteAction)
    {
        SwapDirection();
    }
}
