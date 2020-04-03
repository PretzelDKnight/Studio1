﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MoveBA : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Moving to BA Range!");

        Character thisChar = BattleManager.instance.currentChar;

        thisChar.Move(AITree.tileToMoveTo);

        return AITreeNodeState.Succeeded;
    }
}
