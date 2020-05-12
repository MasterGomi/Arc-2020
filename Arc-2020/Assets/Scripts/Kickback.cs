﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickback : MonoBehaviour
{
    public float KickbackForce;
    public int KickbackDelay;

    private int _timer;
    private RigidbodyConstraints _originalConstraints;

    private void Start()
    {
        _timer = KickbackDelay;
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;
        Rigidbody ballPhysics = ball.GetComponent<Rigidbody>();
        if (_timer == KickbackDelay)
        {
            _originalConstraints = ballPhysics.constraints;
            ballPhysics.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (_timer <= 0)
        {
            ballPhysics.constraints = _originalConstraints;
            ball.GetComponent<Rigidbody>().AddForce(0, KickbackForce, 0, ForceMode.Impulse);
        }
        _timer--;
    }
    private void OnTriggerExit(Collider other)
    {
        _timer = KickbackDelay;
    }
}
