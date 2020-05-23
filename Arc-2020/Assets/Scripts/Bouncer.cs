using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float BounceForce;
    public int Score;

    private void Start()
    {
        TableManager.Manager.RegisterScores(this, Score);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject ball = collision.gameObject;
        if (!ball.CompareTag("Ball")) return;

        // Get a unit vector representing the normal of the collision
        Vector3 normal = -collision.contacts[0].normal.normalized;
        Debug.Log(normal);
        // Add BounceForce in the direction of the normal
        ball.GetComponent<Rigidbody>().AddForce(normal * BounceForce, ForceMode.Impulse);
        // Score
        TableManager.Manager.Score(this);
    }
}
