using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class AttackLowest : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        BattleManager.instance.ResetEverything();

        List<Character> possibleTargets = BattleManager.instance.allies.Members();
        
        Character lowest = null;

        float[] healths = new float[possibleTargets.Count];

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            healths[i] = possibleTargets[i].health.runTimeValue;
        }

        float lowestHP = healths.Min();

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            if (possibleTargets[i].health.runTimeValue == lowestHP)
            {
                lowest = possibleTargets[i];
            }
        }

        AITree.AIstarget = lowest;

        return AITreeNodeState.Succeeded;
    }
}