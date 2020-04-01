using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasS1Energy : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.Skill1Energy())
            return AITreeNodeState.Succeeded;
        else
            return AITreeNodeState.Failed;
    }
}