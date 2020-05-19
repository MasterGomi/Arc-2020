using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetManager : MonoBehaviour
{
    /// <summary>
    /// The list of drop targets which are grouped together by this manager.
    /// </summary>
    private DropTargetComponent[] dropTargets;

    /// <summary>
    /// The score which is added when all drop targets are knocked down.
    /// </summary>
    [SerializeField] int bonusScoreValue;

    void Start()
    {
        dropTargets = GetComponentsInChildren<DropTargetComponent>();
        TableManager.Manager.RegisterScores(this, bonusScoreValue);
    }

    /// <summary>
    /// Checks to see if all targets have been dropped. 
    /// </summary>
    public void CheckForActiveTargets()
    {
        //Check for all target components to see if they are active.
        foreach(DropTargetComponent targetComponent in dropTargets)
        {
            if(targetComponent.Active == true)
            {
                return;
            }
        }

        //Now that we know they are not active, set them all to active.
        foreach (DropTargetComponent targetComponent in dropTargets)
        {
            targetComponent.ResetTarget();
        }

        TableManager.Manager.Score(this);
    }
}
