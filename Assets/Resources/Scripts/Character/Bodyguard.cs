using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodyguard : Character
{
    private float TimeToFire;

    private GameObject effectToSpawn;
    public GameObject FirePoint;

    public List<GameObject> vfx = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        effectToSpawn = vfx[0];
        myTree = new AITree(new Selector(new List<AITreeNode> { new Sequence(new List<AITreeNode> { new HasEnergy(), new Selector(new List<AITreeNode> {
            new AttackMostFatal(new Selector(new List<AITreeNode> { new HasS2Energy(new InS2Range(new AIS2())), new HasS1Energy(new InS1Range(new AIS1())),
                new HasBAEnergy(new InBARange(new AIBasic())), new HasMoveBAEnergy(new MoveBA()) })), new AttackLowest(new Selector(new List<AITreeNode>
                { new HasS2Energy(new InS2Range(new AIS2())), new HasS1Energy(new InS1Range(new AIS1())), new HasBAEnergy(new InBARange(new AIBasic())),
                    new HasMoveBAEnergy(new MoveBA()) })), new AttackNearest(new Selector(new List<AITreeNode> { new HasS2Energy(new InS2Range(new AIS2())),
                        new HasS1Energy(new InS1Range(new AIS1())), new HasBAEnergy(new InBARange(new AIBasic())), new Sequence(new List<AITreeNode>(){new PickATile(), new MoveBA() }) })) }) }), new DoNothing() }));
    }

    public override void Move(HexTile tile)
    {
        StartCoroutine(MoveDownPath(Pathfinder.instance.FindPath(GetCurrentTile(), tile)));

        BattleUIScript.instance.tempUIforInfo.text = this.name + " has moved to tile " + tile.tileID;

        energy.runTimeValue -= tile.energyCost;
    }

    public override void Attack(HexTile tile)
    {
        Character target = tile.occupant;

        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            TimeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().FireRate;
            SpawnVFX();

            Debug.Log(this.name + " is attacking the " + target.name + "!");

            target.health.runTimeValue -= 1;

            Debug.Log(target.name + "'s health is now: " + target.health.runTimeValue);

            BattleUIScript.instance.tempUIforInfo.text = this.name + " attacked the " + target.name + "! " + target.name + "'s health is now: " + target.health.runTimeValue;

            energy.runTimeValue -= AttackEnergy();
        }

        BattleManager.instance.NextMove();
    }

    public override void SkillOne(HexTile tile)
    {
        Character target = tile.occupant;

        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            TimeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().FireRate;
            SpawnVFX();

            Debug.Log(this.name + " is attacking the " + target.name + "!");

            target.health.runTimeValue -= 2;

            Debug.Log(target.name + "'s health is now: " + target.health.runTimeValue);

            BattleUIScript.instance.tempUIforInfo.text = this.name + " attacked the " + target.name + "! " + target.name + "'s health is now: " + target.health.runTimeValue;

            energy.runTimeValue -= Skill1Energy();
        }

        BattleManager.instance.NextMove();
    }

    public override void SkillTwo(HexTile tile)
    {
        Character target = tile.occupant;

        if (target != null)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            TimeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().FireRate;
            SpawnVFX();

            Debug.Log(this.name + " is attacking the " + target.name + "!");

            target.health.runTimeValue -= 4;

            Debug.Log(target.name + "'s health is now: " + target.health.runTimeValue);

            BattleUIScript.instance.tempUIforInfo.text = this.name + " attacked the " + target.name + "! " + target.name + "'s health is now: " + target.health.runTimeValue;

            energy.runTimeValue -= Skill2Energy();
        }

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

    void SpawnVFX()
    {
        GameObject vfx;

        if (FirePoint != null)
        {
            vfx = Instantiate(effectToSpawn, FirePoint.transform.position, Quaternion.identity);

            if (FirePoint != null)
            {
                vfx.transform.localRotation = FirePoint.transform.rotation;
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
    public override void AIMove(HexTile tile)
    {
        AIMoveAcrossPath(Pathfinder.instance.FindPath(GetCurrentTile(), tile));

        BattleUIScript.instance.tempUIforInfo.text = this.name + " has moved to tile " + tile.tileID;

        energy.runTimeValue -= tile.energyCost;
    }
}
