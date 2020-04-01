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
        Character currChar = BattleManager.instance.currentChar;

        List<HexTile> temp = TileManager.instance.ReturnTilesWithinRange(currChar, currChar.stats.skill2range);

        foreach (var tile in temp)
        {
            if (tile.occupant == AITree.AIstarget)
            {
                child.Execute();
                return AITreeNodeState.Succeeded;
            }
        }
        return AITreeNodeState.Failed;
    }
}