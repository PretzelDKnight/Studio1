using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsonist : Character
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

        target.health.runTimeValue -= 10;

        Debug.Log("The enemy's health is now: " + target.health.runTimeValue);

        energy.runTimeValue -= AttackEnergy();
    }

    public override void SkillOne(HexTile tile)
    {
        Debug.Log("I am using Skill1!");

        List<HexTile> affectedTiles = tile.ReturnNeighbours();
        affectedTiles.Add(tile);

        //#Loop to add tiles in range of area of effect, currently only runs twice but can be made dynamic depending on range
        //---------------------------------------------------
        for (int i = 0; i < affectedTiles.Count; i++)
        {
            List<HexTile> temp = affectedTiles[i].ReturnNeighbours();
            for (int j = 0; j < affectedTiles.Count; j++)
            {
                for (int k = 0; k < temp.Count; k++)
                {
                    if (temp[k].tileID != affectedTiles[j].tileID)
                        affectedTiles.Add(temp[k]);
                }
            }
        }
        //---------------------------------------------------

        List<Character> affectedCharacters = new List<Character>();

        //#Loop to make list of affected characters
        //---------------------------------------------------
        for (int i = 0; i < affectedCharacters.Count; i++)
        {
            affectedCharacters.Add(affectedTiles[i].occupant);
        }
        //---------------------------------------------------

        //#Loop to damage affected characters
        //---------------------------------------------------
        for (int i = 0; i < affectedCharacters.Count; i++)
        {
            affectedCharacters[i].health.runTimeValue -= 25;
        }
        //---------------------------------------------------

        energy.runTimeValue -= Skill1Energy();
    }

    public override void SkillTwo(HexTile tile)
    {
        Debug.Log("I am using Skill2!");

        List<HexTile> affectedTiles = tile.ReturnNeighbours();
        affectedTiles.Add(tile);

        //#Loop to add tiles in range of area of effect, currently only runs twice but can be made dynamic depending on range
        //---------------------------------------------------
        for (int i = 0; i < affectedTiles.Count; i++)
        {
            List<HexTile> temp = affectedTiles[i].ReturnNeighbours();
            for (int j = 0; j < affectedTiles.Count; j++)
            {
                for (int k = 0; k < temp.Count; k++)
                {
                    if (temp[k].tileID != affectedTiles[j].tileID)
                        affectedTiles.Add(temp[k]);
                }
            }
        }
        //---------------------------------------------------

        List<Character> affectedCharacters = new List<Character>();

        //#Loop to make list of affected characters
        //---------------------------------------------------
        for (int i = 0; i < affectedCharacters.Count; i++)
        {
            affectedCharacters.Add(affectedTiles[i].occupant);
        }
        //---------------------------------------------------

        this.SkillPush(tile);   //Moves to selected tile

        float reduceBy = this.baseStats.health / this.health.runTimeValue;  //Reduces damage inflicted depending on current health

        //#Loop to damage affected characters
        //---------------------------------------------------
        for (int i = 0; i < affectedCharacters.Count; i++)
        {
            affectedCharacters[i].health.runTimeValue -= 40/reduceBy;
        }
        //---------------------------------------------------

        this.health.runTimeValue = 0;   //Damages self

        energy.runTimeValue -= Skill2Energy();
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
