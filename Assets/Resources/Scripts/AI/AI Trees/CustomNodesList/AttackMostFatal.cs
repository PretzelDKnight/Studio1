using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackMostFatal : AITreeNode
{
    private AITreeNode child;

    public AttackMostFatal(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        Character[] possibleTargets = BattleManager.instance.allies.Members();

        Character fatalest = possibleTargets[0];

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].health.runTimeValue < fatalest.health.runTimeValue)
            {
                fatalest = possibleTargets[i];
            }
        }

        AITree.AIstarget = fatalest;

        //In progress.....

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