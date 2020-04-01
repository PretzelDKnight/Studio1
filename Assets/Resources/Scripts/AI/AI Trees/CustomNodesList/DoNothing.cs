using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoNothing : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        throw new System.NotImplementedException();
    }
}
