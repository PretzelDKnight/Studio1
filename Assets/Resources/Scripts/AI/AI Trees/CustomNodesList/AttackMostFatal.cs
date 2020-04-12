using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AttackMostFatal : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        //AI need not check this condition if its not an arsonist
        //--------------------------------------------------------------------
        if (BattleManager.instance.currentChar.GetType() != typeof(Arsonist))
            return AITreeNodeState.Failed;
        //--------------------------------------------------------------------

        BattleManager.instance.ResetEverything();

        List<Character> possibleTargets = BattleManager.instance.allies.Members();

        Character fatalest = null;

        float[] healths = new float[possibleTargets.Count];

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            healths[i] = possibleTargets[i].health.runTimeValue;
        }

        float fatalestHP = healths.Min();

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            if (possibleTargets[i].health.runTimeValue == fatalestHP)
            {
                fatalest = possibleTargets[i];
            }
        }

        AITree.AIstarget = fatalest;


        return AITreeNodeState.Succeeded;
    }
}