using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : AITreeNode
{
    List<AITreeNode> children = new List<AITreeNode>();

    int lastChild;

    //Constructor for assigning children
    public Selector(List<AITreeNode> nodes)
    {
        children = nodes;
    }

    /* If a child succeeds selector succeeds immediately else it fails*/
    public override AITreeNodeState Execute()
    {
        for (int i = lastChild; i < children.Count; i++)
        {
            switch (children[i].Execute())
            {
                case AITreeNodeState.Succeeded:
                    {
                        currNodeState = AITreeNodeState.Succeeded;
                        lastChild = 0;
                        return currNodeState;
                    }
                case AITreeNodeState.Running:
                    {
                        currNodeState = AITreeNodeState.Running;
                        lastChild = i;
                        return currNodeState;
                    }
            }
        }
        currNodeState = AITreeNodeState.Failed;
        lastChild = 0;
        return currNodeState;
    }
}

