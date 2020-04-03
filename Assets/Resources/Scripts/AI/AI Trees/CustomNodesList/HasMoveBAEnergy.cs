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

        for (int i = 0; i < movableTiles.Count; i++)
        {
            List<HexTile> temp = movableTiles[i].ReturnNeighbours();
            for (int j = 0; j < temp.Count; j++)
            {
                if (temp[j].occupant == AITree.AIstarget)
                {
                    AITree.tileToMoveTo = movableTiles[i];
                    return AITreeNodeState.Succeeded;
                }
            }
        }
        return AITreeNodeState.Failed;
    }
}
