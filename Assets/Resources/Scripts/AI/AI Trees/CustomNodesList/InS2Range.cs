using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InS2Range : AITreeNode
{
    private AITreeNode child;
    public InS2Range(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        Debug.Log("Checking if target in S2 Range");

        Character currChar = BattleManager.instance.currentChar;

        List<HexTile> temp = TileManager.instance.ReturnTilesWithinRange(currChar, currChar.stats.skill2range);

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].occupant == AITree.AIstarget)
            {                
                return child.Execute();
            }
        }
        return AITreeNodeState.Failed;
    }
}