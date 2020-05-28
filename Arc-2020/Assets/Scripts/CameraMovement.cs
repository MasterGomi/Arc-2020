using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Ball;
    private Rigidbody rb;

    void Update()
    {
        Ball = GameObject.FindWithTag("Ball");
        rb = Ball.GetComponent<Rigidbody>();
        if (rb.position.y > -0.8 && rb.position.y < 4.5)
        {
            this.transform.position = new Vector3(0, rb.position.y, -13.74f);
        }
    }
}
