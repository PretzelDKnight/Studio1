using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

        Character fatalest = possibleTargets[Random.Range(0, possibleTargets.Length - 1)];

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].health.runTimeValue < fatalest.health.runTimeValue)
            {
                fatalest = possibleTargets[i];
            }
        }

        Debug.Log("Fatalest is: " + fatalest);
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