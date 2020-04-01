using UnityEngine;
using System.Collections;
using System.Net;

public class Looper : AITreeNode
{
    /* Child node to evaluate */
    private AITreeNode child;

    public AITreeNode node
    {
        get { return child; }
    }

    /* The constructor requires the child node that this inverter decorator has*/
    public Looper(AITreeNode node)
    {
        child = node;
    }

    /* Reports a success if the child fails and a failure if the child succeeeds. Running will report as running */
    public override AITreeNodeState Execute()
    {
        looppoint:
        switch(child.Execute())
        {
            case AITreeNodeState.Failed:
                currNodeState = AITreeNodeState.Failed;
                return currNodeState;
            case AITreeNodeState.Succeeded:
                child.Execute();
                goto looppoint;
            case AITreeNodeState.Running:
                currNodeState = AITreeNodeState.Running;
                return currNodeState;
        }
        currNodeState = AITreeNodeState.Succeeded;
        return currNodeState;
    }
}