using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoNothing : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("AI Cannot do anything");

        BattleManager.instance.Pass();

        return AITreeNodeState.Failed;
    }
}
