using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickback : MonoBehaviour, IController
{
    public float KickbackForce;
    public int KickbackDelay;

    private int _timer;
    private List<INotify> _subscribers = new List<INotify>();

    private void Start()
    {
        _timer = KickbackDelay;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;
        Rigidbody ballPhysics = ball.GetComponent<Rigidbody>();
        if (_timer == KickbackDelay) ballPhysics.constraints = RigidbodyConstraints.FreezeAll;
        else if (_timer <= 0)
        {
            ballPhysics.constraints = RigidbodyConstraints.FreezePositionZ;
            ball.GetComponent<Rigidbody>().AddForce(0, KickbackForce, 0, ForceMode.Impulse);
            Notify(EventNotify.GateDown);
        }
        _timer--;
    }
    private void OnTriggerExit(Collider other)
    {
        _timer = KickbackDelay;
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
