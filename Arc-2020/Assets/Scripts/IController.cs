using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designates an object that can notify others about certain events.
/// Such as a rollover notifing a gate to drop
/// </summary>
public interface IController
{
    /// <summary>
    /// Subcribe to this controller's notifications. Subscriber must implement INotify
    /// </summary>
    /// <param name="subscriber">The object subscribing. Use "this" keyword</param>
    void Subscribe(INotify subscriber);
    /// <summary>
    /// Unsubscribe from this controller's notifications
    /// </summary>
    /// <param name="subscriber">The object unsusbscribing. Use "this" keyword</param>
    void Unsubscribe(INotify subscriber);
}
