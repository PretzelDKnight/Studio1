using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasBAEnergy : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.AttackEnergy())
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}

