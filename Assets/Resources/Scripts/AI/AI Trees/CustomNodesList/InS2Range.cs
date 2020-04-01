using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InS2Range : AITreeNode
{
    private AITreeNode child;
    public InS2Range(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.stats.skill2range)
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}