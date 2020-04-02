using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodyguard : Character
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

    public override void Attack(HexTile tile)
    {
        Character target = tile.occupant;

        Debug.Log(this + " is attacking the " + target + "!");

        target.health.runTimeValue -= 1;

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= AttackEnergy();

        BattleManager.instance.NextMove();
    }

    public override void SkillOne(HexTile tile)
    {
        Character target = tile.occupant;

        Debug.Log(this + " is attacking the " + target + "!");

        target.health.runTimeValue -= 1;

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= Skill1Energy();

        BattleManager.instance.NextMove();
    }

    public override void SkillTwo(HexTile tile)
    {
        Character target = tile.occupant;

        Debug.Log(this + " is attacking the " + target + "!");

        target.health.runTimeValue -= 1;

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= Skill2Energy();

        BattleManager.instance.NextMove();
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
        return stats.skill1Energy;
    }

    public override int Skill2Energy()
    {
        return stats.skill2Energy;
    }

}
