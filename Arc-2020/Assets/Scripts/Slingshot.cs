using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    /// <summary>
    /// The velocity multiplier on hitting a slingshot
    /// </summary>
    [SerializeField] float velocityMultiplier = 4;
    public int Score;

    private void Start()
    {
        if(Score > 0)
        {
            TableManager.Manager.RegisterScores(this, Score);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject ball = collision.collider.gameObject;
        if (!ball.CompareTag("Ball")) return;

        //Right direction is the red axis arrow (which is X in editor), so we take the transform.right of the collider.
        Vector3 direction = transform.right;
        Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();

        ballRigidBody.velocity = direction * velocityMultiplier;

        TableManager.Manager.Score(this);
    }
}
