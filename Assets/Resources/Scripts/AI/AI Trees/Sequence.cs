using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : AITreeNode
{
    List<AITreeNode> children = new List<AITreeNode>();

    int lastChild = 0;

    //Constructor for assigning children
    public Sequence(List<AITreeNode> nodes)
    {
        children = nodes;
    }

    public override AITreeNodeState Execute()
    {
        for (int i = lastChild; i < children.Count; i++)
        {
            switch (children[i].Execute())
            {
                case AITreeNodeState.Failed:
                    {
                        currNodeState = AITreeNodeState.Failed;
                        lastChild = 0;
                        return currNodeState;
                    }
                case AITreeNodeState.Running:
                    {
                        lastChild = i;
                        currNodeState = AITreeNodeState.Running;
                        return currNodeState;
                    }
            }            
        }

        currNodeState = AITreeNodeState.Succeeded;
        lastChild = 0;
        return currNodeState;
    }
}
