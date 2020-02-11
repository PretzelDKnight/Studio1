using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : GOAPAction
{
    bool attacked = false;
    public override bool CheckExecuted()
    {
        return attacked;
    }

    public override bool CheckPrecon(DummyCharacter chara)
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(chara.transform.position, chara.range);
        int dist = 100;
        foreach (var item in hitColliders)
        {
            if (item.tag != chara.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                if (dist > Vector3.Distance(chara.transform.position, temp.transform.position))
                    dummy = temp;
            }
        }

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false;
    }

    public override void Execute(DummyCharacter chara)
    {
        chara.BasicAttack(target);
        chara.energy -= energyCost;
        target.health -= chara.damage - target.armour;
        attacked = true;
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
