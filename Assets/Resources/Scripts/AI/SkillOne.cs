using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOne : GOAPAction
{
    bool attacked = false;

    private void Start()
    {
        AddEffect("kAttack", true);
        AddPrecon("kFindTarget", false);
    }

    public override bool CheckExecuted()
    {
        return attacked;
    }

    public override bool CheckProceduralPrecon(DummyCharacter chara)
    {
        if (chara.energy >= energyCost && InRange)
            return true;
        return false;
    }

    public override bool Execute(DummyCharacter chara)
    {
        chara.SkillOne(target);
        chara.energy -= energyCost;
        target.health -= chara.damage - target.armour;
        attacked = true;
        return true;
    }

    public override bool NeedsEnergy()
    {
        return true;
    }

    public override bool NeedsRange()
    {
        return true;
    }

    public override void Reset()
    {
        attacked = false;
    }
}
