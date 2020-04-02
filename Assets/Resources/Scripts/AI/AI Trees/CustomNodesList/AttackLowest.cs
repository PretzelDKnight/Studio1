using System.Collections;
using System.Collections.Generic;
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
        Character[] possibleTargets = BattleManager.instance.allies.Members();

        Character lowest = possibleTargets[Random.Range(0, possibleTargets.Length - 1)];

        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].health.runTimeValue < lowest.health.runTimeValue)
            {
                lowest = possibleTargets[i];
            }
        }

        Debug.Log("Lowest is: " + lowest);
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