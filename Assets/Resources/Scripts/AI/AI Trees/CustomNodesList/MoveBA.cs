﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MoveBA : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Moving to BA Range!");

        Character thisChar = BattleManager.instance.currentChar;

        thisChar.AIMove(thisChar.myTree.tileToMoveTo);

        Debug.Log("AI's current tile is: " + thisChar.GetCurrentTile().tileID + " and AI's destination tile is: " + thisChar.myTree.tileToMoveTo.tileID);

        if (thisChar.GetCurrentTile() != thisChar.myTree.tileToMoveTo)
            return AITreeNodeState.Running;

        return AITreeNodeState.Succeeded;
    }
}
