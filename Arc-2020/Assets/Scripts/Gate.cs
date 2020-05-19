using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, INotify
{
    public bool DefaultUp;
    public List<GameObject> Controllers;
    public float MovementAmount = 2.9f;
    /// <summary>
    /// Vector used to alter gate's scale when lowered. In positive direction, based on local scale
    /// </summary>
    public Vector3 Scaling = new Vector3(0, 0, 0.1f);

    private bool _positionDown = false;
    private Transform _trans;

    // Start is called before the first frame update
    void Start()
    {
        _trans = gameObject.transform;
        // Lower the gate if that is its default position
        if (!DefaultUp) Lower();
        // Subscribe to the controllers, if possible
        foreach(GameObject obj in Controllers)
        {
            IController controller = obj.GetComponent<IController>();
            // Ensure that we can subscribe to this object, logging a warning if we can't
            if(controller == null)
            {
                Debug.LogWarning("This object doesn't have a component that implements IController", obj.gameObject);
                continue;
            }
            controller.Subscribe(this);
        }
    }

    public void Notify(EventNotify notify)
    {
        switch (notify)
        {
            case EventNotify.GateDown:
                if (!_positionDown) Lower();
                break;
            case EventNotify.GateUp:
                if (_positionDown) Raise();
                break;
            case EventNotify.NewBall:
                if (DefaultUp && _positionDown) Raise();
                else if (!DefaultUp && !_positionDown) Lower();
                break;
        }
    }

    private void Lower()
    {
        if (_positionDown) return;

        // Note: If translate isn't giving acceptable results, try disabling collider instead
        _trans.Translate(0, 0, -MovementAmount);
        _trans.localScale -= Scaling;
        _positionDown = true;
    }

    private void Raise()
    {
        if (!_positionDown) return;

        _trans.localScale += Scaling;
        // Note: If translate isn't giving acceptable results, try disabling collider instead
        _trans.Translate(0, 0, MovementAmount);
        _positionDown = false;
    }
}
