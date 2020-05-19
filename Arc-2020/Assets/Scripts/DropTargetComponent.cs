using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetComponent : MonoBehaviour
{

    public bool Active { get; private set; }

    private DropTargetManager targetManager;
    private Collider targetCollider;


    private void Start()
    {
        targetManager = transform.parent.GetComponent<DropTargetManager>();
        targetCollider = GetComponent<Collider>();

        Active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;

        Debug.Log("Hit a Drop Target");

        if(Active)
        {
            DropTarget();
        }


    }

    public void DropTarget()
    {
        Active = false;

        targetCollider.enabled = false;
        transform.localScale = new Vector3(0.6f, 0.02f, 0.6f);

        if(targetManager != null)
        {
            targetManager.CheckForDroppedTargets();
        }
    }

    public void ResetTarget()
    {
        targetCollider.enabled = true;
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        Active = true;
    }
}
