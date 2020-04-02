using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIBasic : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Using BA!");

        BattleManager.instance.currentChar.Attack(AITree.AIstarget.GetCurrentTile());
        return AITreeNodeState.Succeeded;
    }
}