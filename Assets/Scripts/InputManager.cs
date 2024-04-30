using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour , IDragHandler,IPointerDownHandler
{
    private float _horizontal;
    private bool _firstClick = true;
    
    public delegate void HorizontalInput (float horizontal);
    public static event HorizontalInput OnDragging;
    

    public void OnDrag(PointerEventData eventData)
    {
        _horizontal= eventData.delta.x;
        OnDragging?.Invoke(_horizontal);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_firstClick)
        {
            EventManager.Trigger(EventList.GameStarted);
            _firstClick = false;
        }
    }
}
