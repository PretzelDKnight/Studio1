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

        if (temp == null)
            return AITreeNodeState.Running;

        if (!temp.Occupied)
        {
            thisChar.myTree.tileToMoveTo = temp;
            return AITreeNodeState.Succeeded;
        }
        else if (temp.Occupied)
        {
            for (int i = 0; i < movableTiles.Count; i++)
            {
                if (movableTiles[i] != temp && !movableTiles[i].Occupied)
                {
                    temp = movableTiles[i];
                    break;
                }
            }
        }
        return AITreeNodeState.Succeeded;
    }
}
