using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicAttack : GOAPAction
{
    public override bool CheckAction(Character chara, out HexTile tile, out Character target)
    {
        tile = null;
        return GOAP.EnemyInRange(chara, tile, out target);
    }

    public override bool CheckProceduralPrecondition(Character chara)
    {
        return true;
    }

    public override void Execute(Character chara, HexTile tile, Character target)
    {
        chara.Attack(target);
        Debug.Log("Attacking!");
    }
}
