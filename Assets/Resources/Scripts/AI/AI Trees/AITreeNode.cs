using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AITreeNodeState
{
    Running,
    Succeeded,
    Failed
}

[System.Serializable]
public abstract class AITreeNode
{
    protected AITreeNodeState currNodeState;
    public AITreeNode() { }

    public  AITreeNodeState ReturnCurrentState()
    {
        return currNodeState;
    }   
        
    public abstract AITreeNodeState Execute();
}
