using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanguard : Character
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

        Debug.Log("I am attacking the " + target.tag + "!");

        target.health.runTimeValue -= 20;

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= AttackEnergy();

        BattleManager.instance.NextMove();
    }

    public override void SkillOne(HexTile tile)
    {
        Character target = tile.occupant;

        Debug.Log("I am using Skill1!");

        Debug.Log("I am attacking the " + target.tag + "!");

        target.health.runTimeValue -= 4;

        //#Code to push back enemy
        {
            HexTile tileToPushTo = TileManager.instance.ReturnID(target.GetCurrentTile().tileID - 1); //Will fix this line later
            target.SkillPush(tileToPushTo);
        }

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= Skill1Energy();

        BattleManager.instance.NextMove();
    }

    //In progress..
    //--------------------------------------------
    public override void SkillTwo(HexTile tile)
    {
        Debug.Log("I am using Skill2!");

        energy.runTimeValue -= Skill2Energy();

        BattleManager.instance.NextMove();
    }
    //--------------------------------------------

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
