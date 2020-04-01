using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasS2Energy : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.Skill2Energy())
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}
