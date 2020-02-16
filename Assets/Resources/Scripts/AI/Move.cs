using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : GOAPAction
{
    public override bool CheckProceduralPrecon(Character chara)
    {
        return GOAP.MoveCheck(chara);
    }

    public override void Execute(Character chara)
    {
        //chara.Move(GOAP.ReturnTile());
        Debug.Log("Moving!");
    }
}
