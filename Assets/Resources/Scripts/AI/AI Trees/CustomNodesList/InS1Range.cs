using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InS1Range : AITreeNode
{
    private AITreeNode child;
    public InS1Range(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        Character currChar = BattleManager.instance.currentChar;

        List<HexTile> temp = TileManager.instance.ReturnTilesWithinRange(currChar, currChar.stats.skill1range);

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
