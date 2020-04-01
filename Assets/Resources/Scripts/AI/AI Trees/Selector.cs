using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : AITreeNode
{
    List<AITreeNode> children = new List<AITreeNode>();

    //Constructor for assigning children
    public Selector(List<AITreeNode> nodes)
    {
        children = nodes;
    }

    /* If a child succeeds selector succeeds immediately else it fails*/
    public override AITreeNodeState Execute()
    {
        foreach (AITreeNode node in children)
        {
            switch (node.Execute())
            {
                case AITreeNodeState.Failed:
                    continue;
                case AITreeNodeState.Succeeded:
                    currNodeState = AITreeNodeState.Succeeded;
                    return currNodeState;
                case AITreeNodeState.Running:
                    currNodeState = AITreeNodeState.Running;
                    return currNodeState;
                default:
                    continue;
            }
        }
        currNodeState = AITreeNodeState.Failed;
        return currNodeState;
    }
}
