using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasS2Energy : AITreeNode
{
    private AITreeNode child;

    public HasS2Energy(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.Skill2Energy())
        {
            child.Execute();
            return AITreeNodeState.Succeeded;
        }
        else
            return AITreeNodeState.Failed;
    }
}
