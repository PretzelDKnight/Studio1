using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickATile : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        List<HexTile> movableTiles = TileManager.instance.ReturnTilesToAI(BattleManager.instance.currentChar);

        HexTile temp = movableTiles[Random.Range(0, movableTiles.Count)];

        if (!temp.Occupied)
        {
            AITree.tileToMoveTo = temp;
            return AITreeNodeState.Succeeded;
        }
        return AITreeNodeState.Succeeded;
    }
}
