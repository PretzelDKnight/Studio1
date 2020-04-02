using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIS1 : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Using S1!");

        BattleManager.instance.currentChar.SkillOne(AITree.AIstarget.GetCurrentTile());
        return AITreeNodeState.Succeeded;
    }
}