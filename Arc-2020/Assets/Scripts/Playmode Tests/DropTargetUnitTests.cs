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
        public void DropTargetUnitTestsSimplePasses()
        {
            DropTargetComponent[] dropTargetComponents = GameObject.FindObjectsOfType<DropTargetComponent>();

            foreach (DropTargetComponent dropTargetComponent in dropTargetComponents)
            {
                dropTargetComponent.DropTarget();
                Assert.IsFalse(dropTargetComponent.Active);
            }
        }
    }
}
