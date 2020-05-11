using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControler : MonoBehaviour
{
    public bool IsLeft;
    public float MaxAngle;
    public float HitForce;
    public float Damper;

    private KeyCode _controlKey;
    private JointSpring _spring;
    private HingeJoint _joint;

    // Start is called before the first frame update
    void Start()
    {
        _controlKey = IsLeft ? KeyCode.LeftArrow : KeyCode.RightArrow;
        _spring = new JointSpring
        {
            damper = Damper,
            spring = HitForce
        };
        _joint = GetComponent<HingeJoint>();
        _joint.spring = _spring;
        _joint.useSpring = true;
        JointLimits limits = _joint.limits;
        limits.max = MaxAngle;
        _joint.limits = limits;
        _joint.useLimits = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_controlKey))
        {
            _spring.targetPosition = MaxAngle;
            _joint.spring = _spring;
        }
        else if (Input.GetKeyUp(_controlKey))
        {
            _spring.targetPosition = 0;
            _joint.spring = _spring;
        }
    }
}
