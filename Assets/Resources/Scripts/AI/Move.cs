using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : GOAPAction
{
    bool moved = false;

    private void Start()
    {
        AddEffect("kInRange", true);
    }

    public override bool CheckExecuted()
    {
        return moved;
    }

    public override bool CheckProceduralPrecon(DummyCharacter chara)
    {
        if (chara.energy > 0)
            return true;
        return false;
    }

    public override bool Execute(DummyCharacter chara)
    {
        transform.position += (target.transform.position - chara.transform.position) * chara.speed * Time.deltaTime;

        float distance = Vector3.Distance(target.transform.position, chara.transform.position);

        if (distance <= chara.range)
        {
            InRange = true;
            return true;
        }
        else
        {
            InRange = false;
            return false;
        }
    }

    public override bool NeedsEnergy()
    {
        return true;
    }

    public override bool NeedsRange()
    {
        return false;
    }

    public override void Reset()
    {
        moved = false;
    }
}
