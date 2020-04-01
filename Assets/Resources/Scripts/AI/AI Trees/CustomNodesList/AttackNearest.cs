using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackNearest : AITreeNode
{
    private AITreeNode child;

    public AttackNearest(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        Character[] possibleTargets = BattleManager.instance.allies.Members();

        Character nearest = possibleTargets[0];

        float nearestDist = Vector3.Distance(BattleManager.instance.currentChar.transform.position, possibleTargets[0].transform.position);


        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (Vector3.Distance(BattleManager.instance.currentChar.transform.position, possibleTargets[i].transform.position) < nearestDist)
            {
                nearestDist = Vector3.Distance(BattleManager.instance.currentChar.transform.position, possibleTargets[i].transform.position);
                nearest = possibleTargets[i];
            }
        }

        AITree.AIstarget = nearest;

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