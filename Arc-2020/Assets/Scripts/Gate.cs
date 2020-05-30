using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Gate : MonoBehaviour, INotify
{
    /// <summary>
    /// Is the gate up at the start of the game?
    /// </summary>
    public bool DefaultUp = true;
    /// <summary>
    /// The list of objects that can control the state of this gate
    /// </summary>
    public List<GameObject> Controllers;
    /// <summary>
    /// What material should be used to represent the down position?
    /// [The up position uses whatever material the object is currently using]
    /// </summary>
    public Material DownMaterial;
    /// <summary>
    /// When the gate is informed to raise, how long should it wait. (To let the ball escape)
    /// </summary>
    public float UpDelay;
    /// <summary>
    /// Dictates whether or not the gate resets to default position on a new ball
    /// </summary>
    public bool ResetOnNew = true;

    private bool _positionDown = false;
    private MeshRenderer _render;
    private Material _upMaterial;
    private MeshCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables
        _render = GetComponent<MeshRenderer>();
        _upMaterial = _render.material;
        _collider = GetComponent<MeshCollider>();
        // Lower the gate if that is its default position
        if (!DefaultUp) Lower();
        // Subscribe to TableManager
        TableManager.Manager.Subscribe(this);
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
                // Raise the gate after the appropriate amount of time
                if (_positionDown) StartCoroutine(RaiseAfter(UpDelay));
                break;
            case EventNotify.NewBall:
                if (!ResetOnNew) return;
                if (DefaultUp && _positionDown) Raise();
                else if (!DefaultUp && !_positionDown) Lower();
                break;
        }
    }

    private void Lower()
    {
        if (_positionDown) return;

        _collider.enabled = false;
        _render.material = DownMaterial;
        _positionDown = true;
    }

    private void Raise(object source = null, ElapsedEventArgs e = null)
    {
        if (!_positionDown) return;

        _collider.enabled = true;
        _render.material = _upMaterial;
        _positionDown = false;
    }

    IEnumerator RaiseAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Raise();
    }
}
