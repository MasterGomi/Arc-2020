using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickback : MonoBehaviour, IController
{
    /// <summary>
    /// The magnitude of force to apply to the ball
    /// </summary>
    public float KickbackForce;
    /// <summary>
    /// The amount of time to hold the ball for (seconds)
    /// </summary>
    public float KickbackDelay;

    private float _timer;
    private bool _waiting = false;
    private Rigidbody _ballPhys;

    private List<INotify> _subscribers = new List<INotify>();

    private void Start()
    {
        _timer = KickbackDelay;
    }

    private void Update()
    {
        // Check if we actually need to do things here atm
        if (!_waiting) return;

        // If we're still waiting, update the timer appropriately
        if (_timer > 0)
        {
            // Decrease the timer by the amount of time that has passed since the last update
            _timer -= Time.deltaTime;
            return;
        }

        // Otherwise, do the thing and reset appropriate members
        FireBall();
        _timer = KickbackDelay;
        _waiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;

        _ballPhys = ball.GetComponent<Rigidbody>();
        // Remove the ball's velocity and freeze it in place
        _ballPhys.velocity = Vector3.zero;
        _ballPhys.angularVelocity = Vector3.zero;
        _ballPhys.constraints = RigidbodyConstraints.FreezeAll;
        _waiting = true;
    }

    private void FireBall()
    {
        // Restore the ball's original rigidbody constraints
        _ballPhys.constraints = RigidbodyConstraints.FreezePositionZ;
        _ballPhys.AddForce(0, KickbackForce, 0, ForceMode.Impulse);
        // Notify subscriber (related kickback gate) to raise the gate
        Notify(EventNotify.GateUp);
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
