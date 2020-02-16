using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicAttack : GOAPAction
{
    public override bool CheckProceduralPrecon(Character chara)
    {
        return GOAP.EnemyInRange(chara);
    }

    public override void Execute(Character chara)
    {
        //chara.Attack(GOAP.ReturnTarget());
        Debug.Log("Attacking!");
    }
}
