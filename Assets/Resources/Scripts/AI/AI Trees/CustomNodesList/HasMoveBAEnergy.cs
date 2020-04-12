using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HasMoveBAEnergy : AITreeNode
{
    private AITreeNode child;
    public HasMoveBAEnergy(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        var thisChar = BattleManager.instance.currentChar;

        List<HexTile> movableTiles = TileManager.instance.ReturnTilesToAI(thisChar);

        HexTile temp = movableTiles[Random.Range(0, movableTiles.Count)];

        if (!temp.Occupied)
        {
            thisChar.myTree.tileToMoveTo = temp;
            return child.Execute();
        }
        else
            return AITreeNodeState.Failed;
    }
}
