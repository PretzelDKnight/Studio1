using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoNothing : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("AI Cannot do anything");

        BattleUIScript.instance.tempUIforInfo.text = "AI Cannot do anything and has skipped it's turn";

        BattleManager.instance.Pass();

        return AITreeNodeState.Succeeded;
    }
}
