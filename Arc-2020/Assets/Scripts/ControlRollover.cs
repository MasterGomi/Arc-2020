using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRollover : Rollover, IController
{
    public EventNotify Notification;

    private List<INotify> _subscribers = new List<INotify>();

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Notify(Notification);
    }

    public void Subscribe(INotify subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(INotify subscriber)
    {
        _subscribers.Remove(subscriber);
    }

    private void Notify(EventNotify notify)
    {
        foreach (INotify obj in _subscribers)
        {
            obj.Notify(notify);
        }
    }
}
