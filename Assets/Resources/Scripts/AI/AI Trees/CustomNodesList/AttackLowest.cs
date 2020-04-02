using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class AttackLowest : AITreeNode
{
    private AITreeNode child;

    public AttackLowest (AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        BattleManager.instance.ResetEverything();

        Character[] possibleTargets = BattleManager.instance.allies.Members();
        
        Character lowest = null;

        float[] healths = new float[possibleTargets.Length];

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            healths[i] = possibleTargets[i].health.runTimeValue;
        }

        float lowestHP = healths.Min();

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].health.runTimeValue == lowestHP)
            {
                lowest = possibleTargets[i];
            }
        }

        AITree.AIstarget = lowest;

        switch (child.Execute())
        {
            case AITreeNodeState.Failed:
                return AITreeNodeState.Failed;
            case AITreeNodeState.Succeeded:
                return AITreeNodeState.Succeeded;
            case AITreeNodeState.Running:
                return AITreeNodeState.Running;
            default:
                return AITreeNodeState.Succeeded;
        }
    }
}