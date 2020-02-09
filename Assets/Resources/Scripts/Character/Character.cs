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
    public IntVariable energy;
    public FloatVariable health;
    public Stats stats;

    Status status = Status.Normal;
    bool AI = false;
    float time;
    bool targetable = false;
    bool hovered = false;
    bool selected = false;
    float current = 0;

    public static float checkTileRange = 3f;

    HexTile currentTile = null;

    Renderer render;
    Color normal = Color.white;

    static public GameEvent finishMove;

    public abstract void Attack(Character target);
    public abstract void SkillOne(Character target);
    public abstract void SkillTwo(Character target);

    protected void Awake()
    {
        render = GetComponent<MeshRenderer>();
        current = Shader.PropertyToID("_Current");
        finishMove = Resources.Load<GameEvent>("ScriptableObject/Events/FinishMove");
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

    protected void Initialize()
    {
        health = ScriptableObject.CreateInstance<FloatVariable>();
        energy = ScriptableObject.CreateInstance<IntVariable>();
        stats = ScriptableObject.CreateInstance<Stats>();
        health.Copy(baseStats.health);
        energy.Copy(baseStats.energy);
        stats.Copy(baseStats);
    }

    public HexTile GetCurrentTile()
    {
        return currentTile;
    }

    public void MoveToNearestTile()
    {
        HexTile destination = ReturnNearestUnoccupiedTile();
        destination.Occupied = true;
        Vector3 currentPos = transform.position;
        time = 0;
        StartCoroutine(MoveToTile(currentPos, destination));
    }

    public IEnumerator MoveToTile(Vector3 currentPos, HexTile destination)
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
        finishMove.Raise();
        yield return null;
    }

    public IEnumerator MoveDownPath(List<HexTile> path)
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
        finishMove.Raise();
        yield return null;
    }

    protected void VangaurdAttack(Character target)
    {

    }

    protected void VangaurdSkillOne()
    {

    }

    protected void VangaurdSkillTwo()
    {

    }

    protected void GunnerAttack()
    {

    }

    protected void GunnerSkillOne()
    {

    }

    protected void GunnerSkillTwo()
    {

    }

    protected void ArcanistAttack()
    {

    }

    protected void ArcanistSkillOne()
    {

    }

    protected void ArcanistSkillTwo()
    {

    }

    public void BeginTurn()
    {

    }

    public void EndTurn()
    {

    }

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

                if (!temp.Occupied)
                {
                    if (Vector3.Distance(transform.position, temp.transform.position) < Vector3.Distance(transform.position, lowest.transform.position))
                        lowest = temp;
                }
            }
        }
        return lowest;
    }

    public void RefreshEnergy()
    {
        energy.runTimeValue = baseStats.energy;
    }

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

    public void SetSelected()
    {
        selected = true;
        render.material.color = BattleManager.instance.whenSelected;
    }

    public void ResetCharaValues()
    {
        selected = false;
        hovered = false;
        targetable = false;
        render.material.color = normal;
    }

    public void SetShown()
    {
        current = 1;
    }

    public void SetNotShown()
    {
        current = 0;
    }

    void PropertyToShader()
    {
        render.material.SetFloat("_Current", current);
    }
}