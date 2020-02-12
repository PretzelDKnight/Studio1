using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearest : GOAPAction
{
    bool checkedNearest = false;

    private void Start()
    {
        AddEffect("kFindTarget", true);
        AddPrecon("kInRange", false);
    }

    public override bool CheckExecuted()
    {
        return checkedNearest;
    }

    public override bool CheckProceduralPrecon(DummyCharacter chara)
    {
        return true;
    }

    public override bool Execute(DummyCharacter chara)
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(chara.transform.position, chara.range);
        int dist = 100;
        foreach (var item in hitColliders)
        {
            if (item.tag != this.tag)
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
        checkedNearest = false;
    }
}
