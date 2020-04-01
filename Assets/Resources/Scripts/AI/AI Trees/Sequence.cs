using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : AITreeNode
{
    List<AITreeNode> children = new List<AITreeNode>();
    
    //Constructor for assigning children
    public Sequence(List<AITreeNode> nodes)
    {
        children = nodes;
    }

    public override AITreeNodeState Execute()
    {
        bool childRun = false;

        foreach (AITreeNode node in children)
        {
            switch (node.Execute())
            {
                case AITreeNodeState.Failed:
                    currNodeState = AITreeNodeState.Failed;
                    return currNodeState;
                case AITreeNodeState.Succeeded:
                    continue;
                case AITreeNodeState.Running:
                    childRun = true;
                    continue;
                default:
                    currNodeState = AITreeNodeState.Succeeded;
                    return currNodeState;
            }
        }

        if (childRun)
            currNodeState = AITreeNodeState.Running;
        else
            currNodeState = AITreeNodeState.Succeeded;

        return currNodeState;
    }
}
