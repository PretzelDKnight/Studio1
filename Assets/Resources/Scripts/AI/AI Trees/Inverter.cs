using UnityEngine;
using System.Collections;

public class Inverter : AITreeNode
{
    /* Child node to evaluate */
    private AITreeNode child;

    public AITreeNode node
    {
        get { return child; }
    }

    /* The constructor requires the child node that this inverter decorator has*/
    public Inverter(AITreeNode node)
    {
        child = node;
    }

    /* Reports a success if the child fails and a failure if the child succeeeds. Running will report as running */
    public override AITreeNodeState Execute()
    {
        switch (child.Execute())
        {
            case AITreeNodeState.Failed:
                currNodeState = AITreeNodeState.Succeeded;
                return currNodeState;
            case AITreeNodeState.Succeeded:
                currNodeState = AITreeNodeState.Failed;
                return currNodeState;
            case AITreeNodeState.Running:
                currNodeState = AITreeNodeState.Running;
                return currNodeState;
        }
        currNodeState = AITreeNodeState.Succeeded;
        return currNodeState;
    }
}