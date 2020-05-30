using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour, INotify
{
    private Rigidbody rb;

    private void Start()
    {
        TableManager.Manager.Subscribe(this);
    }

    void Update()
    {
        if (ReferenceEquals(rb, null)) return;

        if (rb.position.y > -0.8 && rb.position.y < 4.3)
        {
            transform.position = new Vector3(0, rb.position.y, -13.74f);
        }
    }

    /// <summary>
    /// Set ball for camera to track.
    /// </summary>
    /// <param name="ball">Ball GameObject to track</param>
    public void FocusBall(GameObject ball)
    {
        if (!ball.CompareTag("Ball")) return;

        rb = ball.GetComponent<Rigidbody>();
    }

    public void Notify(EventNotify notify)
    {
        if(notify == EventNotify.EndOfBall)
        {
            rb = null;
        }
    }
}
