using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrain : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;

        // Give the ball a second to fall off the table, then destroy it
        Destroy(ball, 1);
        TableManager.Manager.DrainBall();
    }
}
