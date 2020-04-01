using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InS1Range : AITreeNode
{
    private AITreeNode child;
    public InS1Range(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.stats.skill1range)
        {
            child.Execute(); return AITreeNodeState.Succeeded;
        }
        else
            return AITreeNodeState.Failed;
    }
}
