using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour
{
    public Rigidbody rb;
    private float t = 0.0f;
    private bool moving = false;

    void OnTriggerEnter(Collider other)
    
        {
        UnityEngine.Debug.Log(this);
        UnityEngine.Debug.Log("good");
        var cubeRenderer = this.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.black);
    }
}
