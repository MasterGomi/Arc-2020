using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public Rigidbody rb;
    private float t = 0.0f;
    private bool moving = false;

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("hit , point value increase?");
        rb.velocity = rb.velocity * 2.5f;
        
    }
}
