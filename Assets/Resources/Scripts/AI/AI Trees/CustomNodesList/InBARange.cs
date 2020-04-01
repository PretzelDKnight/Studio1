using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InBARange : AITreeNode
{
    private AITreeNode child;
    public InBARange(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.stats.attackRange)
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}
