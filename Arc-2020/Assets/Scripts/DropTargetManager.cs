using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargetManager : MonoBehaviour
{
    /// <summary>
    /// The list of drop targets which are grouped together by this manager.
    /// </summary>
    private DropTargetComponent[] dropTargets;


    // Start is called before the first frame update
    void Start()
    {
        dropTargets = GetComponentsInChildren<DropTargetComponent>();    
    }

    public void CheckForDroppedTargets()
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
    }
}
