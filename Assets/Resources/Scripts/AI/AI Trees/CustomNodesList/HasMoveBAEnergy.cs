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
        List<HexTile> movableTiles = TileManager.instance.ReturnTilesToAI(BattleManager.instance.currentChar);

        HexTile temp = movableTiles[Random.Range(0, movableTiles.Count)];

        if (temp != null)
        {
            AITree.tileToMoveTo = temp;
            return child.Execute();
        }
        else
            return AITreeNodeState.Failed;
    }
}
