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
    private RigidbodyConstraints _prevConstraints;
    private float _startingY;
    private float _bottomY;

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
        _prevConstraints = _rigid.constraints; // Should be freeze X and Z movement and all rotation
        _rigid.constraints = RigidbodyConstraints.FreezeAll;
        _startingY = _trans.position.y;
        _bottomY = _startingY - DownDist;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _wasDown = true;
            if(_trans.position.y > _bottomY)
            {
                _trans.Translate(0, -DistStep, 0, Space.World);
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
        }
        else if(_trans.position.y >= _startingY)
        {
            _rigid.constraints = RigidbodyConstraints.FreezeAll;
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

    //Stop the ball from bouncing on collision enter.
    private void OnCollisionEnter(Collision collision)
    {

        GameObject ball = collision.collider.gameObject;
        if (!ball.CompareTag("Ball")) return;
        Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();

        ballRigidBody.velocity = Vector3.zero;

    }
}
