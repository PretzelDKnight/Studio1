using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : GOAPAction
{
    public override bool CheckAction(Character chara, out HexTile tile, out Character target)
    {
        return GOAP.MoveAttackCheck(chara, out tile, out target);
    }

    public override bool CheckProceduralPrecondition(Character chara)
    {
        return true;
    }

    public override void Execute(Character chara, HexTile tile, Character target)
    {
        chara.Move(tile);
        Debug.Log("Moving!");
    }
}
