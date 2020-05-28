using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public int Score;
    private float rotation = 0;
    private const int rotationMultiplier = 2;
    private float ballVelocity;
    
    // Start is called before the first frame update
    private void Start()
    {
        TableManager.Manager.RegisterScores(this, Score);
    }
    void Spin()
    {
        gameObject.transform.Rotate(0, 0,ballVelocity* rotationMultiplier);
        rotation += ballVelocity * rotationMultiplier;
        if(ballVelocity < 0)
        {
            CancelInvoke();
            ballVelocity = 0;
        }
        if(rotation >= 360)
        {
            rotation = 0;
            TableManager.Manager.Score(this);
        }
        ballVelocity -= 0.2f;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        GameObject ball = collision.gameObject;
        if (!ball.CompareTag("Ball")) return;
        InvokeRepeating("Spin", 0.1f, 0.05f);
        ballVelocity = ball.GetComponent<Rigidbody>().velocity.magnitude;
    }
}
