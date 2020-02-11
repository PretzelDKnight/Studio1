using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : GOAPAction
{
    bool moved = false;

    public override bool CheckExecuted()
    {
        return moved;
    }

    public override bool CheckPrecon(DummyCharacter chara)
    {
        return true;
    }

    public override void Execute(DummyCharacter chara)
    {
        chara.Move();
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
