using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DropTargetUnitTests
    {

        //Checks to see if the drop targets are inactive after they've been dropped.
        [Test]
        public void CheckForInactiveTargetAfterDrop()
        {
            DropTargetComponent[] dropTargetComponents = GameObject.FindObjectsOfType<DropTargetComponent>();

            foreach (DropTargetComponent dropTargetComponent in dropTargetComponents)
            {
                dropTargetComponent.DropTarget();
                Assert.IsFalse(dropTargetComponent.Active);
            }
        }

        [Test]
        public void CheckForActiveTargetsAfterAllDropped()
        {
            DropTargetManager[] dropTargetManagers = GameObject.FindObjectsOfType<DropTargetManager>();

            for (int i = 0; i < dropTargetManagers.Length; i++)
            {
                DropTargetManager dropTargetManager = dropTargetManagers[i];

                //Drop all targets within a manager.
                for(int j = 0; j < dropTargetManager.transform.childCount; j++)
                {
                    DropTargetComponent dropTargetComponent = dropTargetManager.transform.GetChild(j).GetComponent<DropTargetComponent>();
                    if (!dropTargetComponent.Active)
                    {
                        dropTargetComponent.DropTarget();
                    }
                }

                //Loop through the targets again and make sure they've reset.
                for (int j = 0; j < dropTargetManager.transform.childCount; j++)
                {
                    DropTargetComponent dropTargetComponent = dropTargetManager.transform.GetChild(j).GetComponent<DropTargetComponent>();

                    Assert.IsTrue(dropTargetComponent.Active);
                }
            }
        }
    }
}
