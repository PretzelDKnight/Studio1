using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickATile : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        var thisChar = BattleManager.instance.currentChar;

        List<HexTile> movableTiles = TileManager.instance.ReturnTilesToAI(thisChar);

        HexTile temp = movableTiles[Random.Range(0, movableTiles.Count)];

        if (!temp.Occupied)
        {
            thisChar.myTree.tileToMoveTo = temp;
            return AITreeNodeState.Succeeded;
        }
        return AITreeNodeState.Succeeded;
    }
}
