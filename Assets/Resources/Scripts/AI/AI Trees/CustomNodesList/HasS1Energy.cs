using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HasS1Energy : AITreeNode
{
    private AITreeNode child;
    public HasS1Energy(AITreeNode node)
    {
        child = node;
    }

    public override AITreeNodeState Execute()
    {
        Debug.Log("Checking if AI has S1 energy");

        if (BattleManager.instance.currentChar.energy.runTimeValue >= BattleManager.instance.currentChar.Skill1Energy())
        {
            return child.Execute();
        }
        else
            return AITreeNodeState.Failed;
    }
}