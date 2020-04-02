using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasBAEnergy : AITreeNode
{
    private AITreeNode child;
    public HasBAEnergy(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.AttackEnergy())
        {
            return child.Execute();
        }
        else
            return AITreeNodeState.Failed;
    }
}

