using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIBasic : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        BattleManager.instance.currentChar.Attack(AITree.AIstarget.GetCurrentTile());
        return AITreeNodeState.Succeeded;
    }
}