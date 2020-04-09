using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MoveBA : AITreeNode
{
    public override AITreeNodeState Execute()
    {
        Debug.Log("Moving to BA Range!");

        Character thisChar = BattleManager.instance.currentChar;
                

        thisChar.transform.position = Vector3.MoveTowards(thisChar.transform.position, AITree.tileToMoveTo.transform.position, 0.1f);

        Debug.Log("AI's current tile is: " + thisChar.GetCurrentTile().tileID + " and AI's destination tile is: " + AITree.tileToMoveTo.tileID);

        if (thisChar.GetCurrentTile() != AITree.tileToMoveTo)
            return AITreeNodeState.Running;

        return AITreeNodeState.Succeeded;
    }
}
