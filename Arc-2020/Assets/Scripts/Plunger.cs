using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour, IController, INotify
{
    struct SpringData
    {
        public Vector3 anchor, connectedAnchor;
        public float spring, damper;
    }

    private SpringData _spring;
    private Transform _trans;
    private Rigidbody _rigid;
    private bool _wasDown = false;
    private float _traveled = 0;
    private RigidbodyConstraints _prevConstraints;
    private Vector3 _previousAnchor;

    public float DownDist;
    public float DistStep;

    // Start is called before the first frame update
    void Start()
    {
        SpringJoint curSpring = GetComponent<SpringJoint>();
        _spring = new SpringData()
        {
            anchor = curSpring.anchor,
            connectedAnchor = curSpring.connectedAnchor,
            spring = curSpring.spring,
            damper = curSpring.damper
        };
        Destroy(curSpring);
        _trans = gameObject.transform;
        _rigid = GetComponent<Rigidbody>();
        _prevConstraints = _rigid.constraints;
        _rigid.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _wasDown = true;
            if(_traveled < DownDist)
            {
                _trans.Translate(0, -DistStep, 0, Space.World);
                _traveled += DistStep;
            }
        }
        else if (_wasDown)
        {
            _rigid.constraints = _prevConstraints;
            SpringJoint newSpring = gameObject.AddComponent<SpringJoint>() as SpringJoint;
            newSpring.autoConfigureConnectedAnchor = false;
            newSpring.anchor = _spring.anchor;
            newSpring.connectedAnchor = _spring.connectedAnchor;
            newSpring.spring = _spring.spring;
            newSpring.damper = _spring.damper;
            newSpring.enableCollision = true;
            _wasDown = false;
            _traveled = 0;
        }
    }

    public void Notify(EventNotify notify)
    {
        throw new System.NotImplementedException();
    }

    public void Subscribe(INotify subscriber)
    {
        throw new System.NotImplementedException();
    }

    public void Unsubscribe(INotify subscriber)
    {
        throw new System.NotImplementedException();
    }
}
