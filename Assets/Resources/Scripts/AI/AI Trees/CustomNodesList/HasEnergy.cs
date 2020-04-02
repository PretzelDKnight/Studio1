using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HasEnergy : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Checking if AI has any energy");

        if (BattleManager.instance.currentChar.energy.runTimeValue > 0)
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}
