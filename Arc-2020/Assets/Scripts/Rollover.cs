using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour, IController
{
    public int Score;
    public EventNotify Notification;

    private List<INotify> _subscribers = new List<INotify>();

    private void Start()
    {
        if (Score > 0) TableManager.Manager.RegisterScores(this, Score);
    }

    void OnTriggerEnter(Collider other)
    {
        if (Score > 0) TableManager.Manager.Score(this);
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
        foreach(INotify obj in _subscribers)
        {
            obj.Notify(notify);
        }
    }
}
