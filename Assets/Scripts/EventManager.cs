using System;
using System.Collections.Generic;
using UnityEngine;



public static class EventManager
{
    
    private static Dictionary<EventList,Action> _eventTable= new Dictionary<EventList, Action>();

    public static void Subscribe(EventList eventName, Action action)
    {
        if (!_eventTable.ContainsKey(eventName))
            _eventTable[eventName] = action;
        else _eventTable[eventName] += action;
    }
    
    public static void Unsubscribe(EventList eventName, Action action)
    {
        if (_eventTable[eventName] != null)
            _eventTable[eventName] -= action;
        if (_eventTable[eventName] == null)
            _eventTable.Remove(eventName);
    }
    
    public static void Trigger(EventList eventName)
    {
        if (_eventTable[eventName] != null)
            _eventTable[eventName]?.Invoke();
    }
    
}
