using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetComponent : MonoBehaviour
{
    /// <summary>
    /// The current state of the drop target. 
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    /// Components used for drop target
    /// </summary>
    private DropTargetManager targetManager;
    private Collider targetCollider;
    
    /// <summary>
    /// The amount of points given when this drop target is hit.
    /// </summary>
    [SerializeField] int scoreValue;
    public int ScoreValue { get { return scoreValue;  } }

    private Vector3 currentScale;


    private void Start()
    {
        targetManager = transform.parent.GetComponent<DropTargetManager>();
        targetCollider = GetComponent<Collider>();

        TableManager.Manager.RegisterScores(this, scoreValue);

        currentScale = transform.localScale;

        Active = true;
    }

    /// <summary>
    /// Collision Detection on a drop target.
    /// </summary>
    /// <param name="other">The other game object</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit a Drop Target a");
        GameObject ball = other.gameObject;
        if (!ball.CompareTag("Ball")) return;

        Debug.Log("Hit a Drop Target");

        if(Active)
        {
            DropTarget();
        }
    }

    /// <summary>
    /// Drops the target.
    /// </summary>
    public void DropTarget()
    {
        Active = false;

        targetCollider.enabled = false;
        transform.localScale = new Vector3(currentScale.x, 0.02f, currentScale.z);

        TableManager.Manager.Score(this);

        if(targetManager != null)
        {
            targetManager.CheckForActiveTargets();
        }
    }

    /// <summary>
    /// Resets the target back to upright.
    /// </summary>
    public void ResetTarget()
    {
        StartCoroutine(EnableHitbox());
    }

    /// <summary>
    /// IEnumerator to delay the target reset process. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableHitbox()
    {
        yield return new WaitForSeconds(0.5f);

        transform.localScale = currentScale;

        targetCollider.enabled = true;

        Active = true;
    }
}
