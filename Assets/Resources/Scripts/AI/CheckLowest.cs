using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLowest : GOAPAction
{
    bool checkedLowest = false;

    private void Start()
    {
        AddEffect("kFindTarget", true);
        AddPrecon("kInRange", false);
    }

    public override bool CheckExecuted()
    {
        return checkedLowest;
    }

    public override bool CheckProceduralPrecon(DummyCharacter chara)
    {
        return true;
    }

    public override bool Execute(DummyCharacter chara)
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(chara.transform.position, chara.range);
        int health = 100;
        foreach (var item in hitColliders)
        {
            if (item.tag != chara.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                if (health > temp.health)
                    dummy = temp;
            }
        }

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false; ;
    }

    public override bool NeedsEnergy()
    {
        return false;
    }

    public override bool NeedsRange()
    {
        return true;
    }

    public override void Reset()
    {
        checkedLowest = false;
    }
}
