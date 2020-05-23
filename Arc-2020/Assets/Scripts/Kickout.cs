using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickout : MonoBehaviour, IController
{
    /// <summary>
    /// Amount of time the ball is held in place for (in seconds)
    /// </summary>
    public float HoldTime;
    /// <summary>
    /// The direction to fire the ball in
    /// </summary>
    /// <remarks>Will be normalised, so the magnitude is irrelevant</remarks>
    public Vector2 FiringDirection;
    /// <summary>
    /// Base speed given to the ball (effectively the new magnitude of FiringDirection)
    /// </summary>
    public float FiringSpeed;
    /// <summary>
    /// The maximum varience angle (degrees) that the firing direction can be randomly altered by
    /// </summary>
    public float DirectionVarience;
    /// <summary>
    /// The maxmimum value by which the speed can be randomly increased or reduced by
    /// </summary>
    public float SpeedVarience;
    /// <summary>
    /// How far inside the hole to hold the ball
    /// </summary>
    public float Depth;
    /// <summary>
    /// How many points to award when the ball is caught
    /// </summary>
    public int Score;

    private Transform _thisTransform;
    private Transform _ballTransform;
    private Rigidbody _ballPhys;
    private bool _waiting = false;
    private float _waitTimer;

    private List<INotify> _subscribers = new List<INotify>();

    private void Start()
    {
        FiringDirection.Normalize();
        _thisTransform = GetComponent<Transform>();
        _waitTimer = HoldTime;
        TableManager.Manager.RegisterScores(this, Score);
    }

    private void Update()
    {
        // Check if we actually need to do things here atm
        if (!_waiting) return;

        // If we're still waiting, update the timer appropriately
        if (_waitTimer > 0)
        {
            // Decrease the timer by the amount of time that has passed since the last update
            _waitTimer -= Time.deltaTime;
            return;
        }

        // Otherwise, do the thing and reset appropriate members
        FireBall();
        _waitTimer = HoldTime;
        _waiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;

        // Get a reference to the ball's Tranform and RigidBody components (we also need to access them in FireBall())
        _ballTransform = ball.transform;
        _ballPhys = ball.GetComponent<Rigidbody>();
        // Work out where to move the ball to so it is inside the hole
        Vector3 moveBy = new Vector3(_thisTransform.position.x - _ballTransform.position.x, _thisTransform.position.y - _ballTransform.position.y, Depth);
        // Move the ball into place
        _ballTransform.Translate(moveBy, Space.World);
        // Stop the ball from moving
        _ballPhys.velocity = Vector3.zero;
        _ballPhys.angularVelocity = Vector3.zero;
        _ballPhys.constraints = RigidbodyConstraints.FreezeAll;
        // Wait to fire
        _waiting = true;

        // Notify subscriber (a kickback gate) to lower the gate
        Notify(EventNotify.GateDown);
        // Score points
        TableManager.Manager.Score(this);
    }

    private void FireBall()
    {
        // Move the ball out of the hole
        _ballTransform.Translate(0, 0, -Depth, Space.World);

        // Set up the firing force direction:
        float angle = Random.Range(-DirectionVarience, DirectionVarience);
        // Set firingForce first to a normalised vector in the correct direction
        // Note FiringFirection is normalised in Start(), so it doesn't need to be called every time
        Vector3 firingForce = Quaternion.Euler(0, 0, angle) * FiringDirection;
        Debug.Log(firingForce);

        // Set up the firing force speed (similar to calculating the angle):
        float speed = FiringSpeed + Random.Range(-SpeedVarience, SpeedVarience);
        // Apply the calculated speed to the firing force
        // i.e. make the normalised vector the right length
        firingForce *= speed;

        Debug.Log(firingForce);

        // Finally, unfreeze the ball and apply the firing force vector to the ball
        // Note: Freezing position on the Z-axis is the default state for the ball, so it doesn't move in ways we don't expect
        _ballPhys.constraints = RigidbodyConstraints.FreezePositionZ;
        _ballPhys.AddForce(firingForce, ForceMode.Impulse);
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
