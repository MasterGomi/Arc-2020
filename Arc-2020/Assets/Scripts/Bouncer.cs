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
        Vector3 normal = -collision.GetContact(0).normal.normalized;
        if(Mathf.Abs(normal.y) > 0.95f)
        {
            //normal.y *= 3;
        }
        Debug.Log(normal);
        // Add BounceForce in the direction of the normal
        ball.GetComponent<Rigidbody>().AddForce(normal * BounceForce, ForceMode.VelocityChange);
        // Score
        TableManager.Manager.Score(this);
    }
}
