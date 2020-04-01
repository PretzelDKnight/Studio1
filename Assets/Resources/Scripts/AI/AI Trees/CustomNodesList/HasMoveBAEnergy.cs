using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasMoveBAEnergy : AITreeNode
{
    private AITreeNode child;
    public HasMoveBAEnergy(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        throw new System.NotImplementedException();
        //In progress....
    }
}
