using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HasEnergy : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue > 0)
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}
