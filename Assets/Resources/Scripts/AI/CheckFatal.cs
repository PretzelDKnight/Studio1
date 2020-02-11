using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFatal : GOAPAction
{
    bool checkedFatal = false;

    // Start is called before the first frame update
    void Start()
    {
        AddEffect("kFindTarget", true);
        AddPrecon("kInRange", false);
    }

    public override bool CheckExecuted()
    {
        return checkedFatal;
    }

    public override bool CheckProceduralPrecon(DummyCharacter chara)
    {
        return false;
    }

    public override bool Execute(DummyCharacter chara)
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(chara.transform.position, chara.range);
        int dmg = 0;
        foreach (var item in hitColliders)
        {
            if (item.tag != this.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                int dmgDeal = chara.damage - temp.armour;
                if (dmgDeal > dmg)
                    dummy = temp;
            }
        }

        checkedFatal = true;

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false;
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
        checkedFatal = true;
    }

}
