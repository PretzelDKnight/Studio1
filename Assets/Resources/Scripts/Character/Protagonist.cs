using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Move(HexTile tile)
    {
        StartCoroutine(MoveDownPath(Pathfinder.instance.FindPath(GetCurrentTile(), tile)));
        energy.runTimeValue -= tile.energyCost;
    }

    public override void Attack(Character target)
    {
        Debug.Log("I am attacking the " + target.tag + "!");
        energy.runTimeValue -= AttackEnergy();

        BattleManager.instance.ResetEverything();
        BattleManager.instance.SetState(State.Attack);
        TileManager.instance.FindTilesWithinRange(target);
    }

    public override void SkillOne(HexTile tile)
    {
        Debug.Log("I am using Skill1!");
        energy.runTimeValue -= Skill1Energy();

        BattleManager.instance.ResetEverything();
        BattleManager.instance.SetState(State.Skill1);
        //TileManager.instance.FindTilesWithinRange(target);
    }

    public override void SkillTwo(HexTile tile)
    {
        Debug.Log("I am using Skill2!");
        energy.runTimeValue -= Skill2Energy();

        BattleManager.instance.ResetEverything();
        BattleManager.instance.SetState(State.Skill2);
        //TileManager.instance.FindTilesWithinRange(target);
    }

    public override int MoveEnergy()
    {
        return 2;
    }

    public override int AttackEnergy()
    {
        return 1;
    }

    public override int Skill1Energy()
    {
        return 3;
    }

    public override int Skill2Energy()
    {
        return 4;
    }
}
