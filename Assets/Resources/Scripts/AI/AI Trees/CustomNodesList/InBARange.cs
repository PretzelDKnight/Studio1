﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InBARange : AITreeNode
{
    private AITreeNode child;
    public InBARange(AITreeNode node)
    {
        child = node;
    }
    public override AITreeNodeState Execute()
    {
        Character currChar = BattleManager.instance.currentChar;

        List<HexTile> temp = TileManager.instance.ReturnTilesWithinRange(currChar, currChar.stats.attackRange);

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].occupant == AITree.AIstarget)
            {
                child.Execute();
                return AITreeNodeState.Succeeded;
            }
        }
        return AITreeNodeState.Failed;
    }
}
