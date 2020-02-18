using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Status
{
    Normal,
    Stunned,
    Staggered
}

public abstract class Character : MonoBehaviour , IComparable
{
    public BaseStats baseStats;
    public CoolDowns baseCD;
    public CoolDowns CD;
    public IntVariable energy;
    public FloatVariable health;
    public Stats stats;

    public List<KVPair> goals;
    public GOAPAction[] actions;

    Status status = Status.Normal;
    public bool AI = false;
    float time;
    bool targetable = false;
    bool hovered = false;
    bool selected = false;
    float current = 0;

    public static float checkTileRange = 3f;

    HexTile currentTile = null;

    Renderer render;
    Color normal = Color.white;

    public abstract void Move(HexTile tile);
    public abstract void Attack(Character target);
    public abstract void SkillOne(HexTile tile);
    public abstract void SkillTwo(HexTile tile);
    public abstract int MoveEnergy();
    public abstract int AttackEnergy();
    public abstract int Skill1Energy();
    public abstract int Skill2Energy();

    protected void Awake()
    {
        render = GetComponent<MeshRenderer>();
        current = 0;
        current = Shader.PropertyToID("_Current");
        normal = render.material.color;
    }

    protected void Update()
    {
        hovered = false;
    }

    protected void LateUpdate()
    {
        PropertyToShader();   
    }

    // Instantiates and assigns Scriptable object
    protected void Initialize()
    {
        health = ScriptableObject.CreateInstance<FloatVariable>();
        energy = ScriptableObject.CreateInstance<IntVariable>();
        stats = ScriptableObject.CreateInstance<Stats>();
        CD = ScriptableObject.CreateInstance<CoolDowns>();
        CD.Reset();
        health.Copy(baseStats.health);
        energy.Copy(baseStats.energy);
        stats.Copy(baseStats);
    }

    // Returns current tile of the character
    public HexTile GetCurrentTile()
    {
        return currentTile;
    }

    // Moves the character to the nearest tile (Called after hexgrid is generated when battle event is raised)
    public void MoveToNearestTile()
    {
        HexTile destination = ReturnNearestUnoccupiedTile();
        destination.Occupied = true;
        Vector3 currentPos = transform.position;
        time = 0;
        StartCoroutine(MoveToTile(currentPos, destination));
    }

    // Coroutine for smoothly moving character to a tile
    protected IEnumerator MoveToTile(Vector3 currentPos, HexTile destination)
    {
        while (time < 1)
        {
            transform.position = Vector3.Lerp(currentPos, destination.ReturnTargetPosition(currentPos), time);
            time += Time.deltaTime * stats.speed;
            yield return null;
        }

        transform.position = destination.ReturnTargetPosition(transform.position);

        currentTile = destination;
        currentTile.Walkable = false;
        BattleManager.instance.NextMove();
        yield return null;
    }

    // Coroutine for smoothly moving character down a list of tiles
    protected IEnumerator MoveDownPath(List<HexTile> path)
    {
        foreach(var tile in path)
        {
            if (tile != currentTile)
            {
                Vector3 currentPos = transform.position;
                while (time < 1)
                {
                    transform.position = Vector3.Lerp(currentPos, tile.ReturnTargetPosition(currentPos), time);
                    time += Time.deltaTime * stats.speed;
                    yield return null;
                }

                if (time >= 1)
                    time = 0;
            }
        }

        transform.position = path[path.Count - 1].ReturnTargetPosition(transform.position);

        currentTile.Occupied = false;
        currentTile = path[path.Count - 1];
        currentTile.Occupied = true;
        currentTile.Walkable = false;
        BattleManager.instance.NextMove();
        yield return null;
    }

    // Compare function for IComparable, ordering by speed of the characters
    public int CompareTo(object obj)
    {
        Character chara = (Character)obj;
        if (stats.speed > chara.stats.speed)
            return 1;
        else if (stats.speed == chara.stats.speed)
            return 0;
        else
            return -1;
    }

    // Returns nearest Unoccupied tile
    HexTile ReturnNearestUnoccupiedTile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkTileRange);
        HexTile temp = null;
        HexTile lowest = null;
        foreach (var item in hitColliders)
        {
            temp = item.GetComponent<HexTile>();
            if (temp != null)
            {
                if (lowest == null)
                    lowest = temp;
                else if (!temp.Occupied)
                {
                    if (Vector3.Distance(transform.position, temp.transform.position) < Vector3.Distance(transform.position, lowest.transform.position))
                        lowest = temp;
                }
            }
        }
        return lowest;
    }

    // Refils energy of the character
    public void RefreshEnergy()
    {
        energy.runTimeValue = baseStats.energy;
    }

    // Sets character as targetable and assigns material color
    public bool Targetable
    {
        get { return targetable; }
        set
        {
            targetable = value;
            if (value)
            {
                render.material.color = BattleManager.instance.whenTargetable;
            }
            else
            {
                render.material.color = normal;
            }
        }
    }

    // Sets character as hovered and assigns material color
    public bool Hovered
    {
        get { return Hovered; }
        set
        {
            targetable = value;
            if (value)
            {
                render.material.color = BattleManager.instance.whenHovered;
            }
            else
            {
                render.material.color = normal;
            }
        }
    }

    // Sets character as selected and assigns material color
    public void SetSelected()
    {
        selected = true;
        render.material.color = BattleManager.instance.whenSelected;
    }

    // Resets character values and material color back to normal
    public void ResetCharaValues()
    {
        selected = false;
        hovered = false;
        targetable = false;
        render.material.color = normal;
    }

    // Sets shader property for character to be outlined
    public void SetShown()
    {
        current = 1;
    }

    // Resets shader property for the character to not have an outline
    public void SetNotShown()
    {
        current = 0;
    }

    // Sends value of shader property back to the shader
    void PropertyToShader()
    {
        render.material.SetFloat("_Current", current);
    }
}