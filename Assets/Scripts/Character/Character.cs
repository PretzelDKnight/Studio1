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
    public Stats stats = null;
    public Stats currentStats = null;
    Status status = Status.Normal;
    bool AI = false;
    bool executing = false;
    float time;
    bool targetable = false;
    bool hovered = false;
    bool selected = false;

    public static float checkTileRange = 3f;

    HexTile currentTile = null;

    Renderer render;
    Color normal = Color.white;

    public abstract void Attack(Character target);
    public abstract void SkillOne(Character target);
    public abstract void SkillTwo(Character target);

    protected void Awake()
    {
        render = GetComponent<MeshRenderer>();
        normal = render.material.color;
    }

    protected void Update()
    {
        hovered = false;
    }

    public HexTile GetCurrentTile()
    {
        currentTile = GetTargetTile(this);
        currentTile.Walkable = false;
        return currentTile;
    }

    protected HexTile GetTargetTile(Character chara)
    {
        HexTile hTile = null;
        RaycastHit hit;
        if (Physics.Raycast(chara.gameObject.transform.position, -Vector3.up, out hit, HexGrid.instance.layerMask))
        {
            hTile = hit.collider.GetComponent<HexTile>();
        }
        return hTile;
    }

    public void MoveToNearestTile()
    {
        if (GetCurrentTile() != null)
        {
            if (!currentTile.Occupied)
            {
                Vector3 currentPos = transform.position;
                time = 0;
                StartCoroutine(MoveToTile(currentPos, currentTile));
            }
            else
            {
                HexTile destination = ReturnNearestUnoccupiedTile();
                Vector3 currentPos = transform.position;
                time = 0;
                StartCoroutine(MoveToTile(currentPos, destination));
            }
        }
    }

    public IEnumerator MoveToTile(Vector3 currentPos, HexTile destination)
    {
        executing = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(currentPos, destination.ReturnPosition(currentPos), time);
            time += Time.deltaTime * stats.speed;
            yield return null;
        }

        transform.position = destination.ReturnPosition(transform.position);

        currentTile = destination;
        currentTile.Occupied = true;
        executing = false;
        yield return null;
    }

    public IEnumerator MoveDownPath(List<HexTile> path)
    {
        executing = true;
        foreach (var item in path)
        {
            Vector3 currentPos = transform.position;
            while (time < 1)
            {
                transform.position = Vector3.Lerp(currentPos, item.ReturnPosition(currentPos), time);
                time += Time.deltaTime * stats.speed;
                yield return null;
            }
        }

        transform.position = path[path.Count - 1].ReturnPosition(transform.position);

        path[0].Occupied = false;
        currentTile = path[path.Count - 1];
        currentTile.Occupied = true;
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
        RefreshEnergy();
        BattleManager.instance.SwitchChara(this);
    }

    public void EndTurn()
    {

    }

    public int CompareTo(object obj)
    {
        Character chara = (Character)obj;
        if (currentStats.speed > chara.currentStats.speed)
            return 1;
        else if (currentStats.speed == chara.currentStats.speed)
            return 0;
        else
            return -1;
    }

    HexTile ReturnNearestUnoccupiedTile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkTileRange);
        HexTile temp = null;
        foreach (var item in hitColliders)
        {
            temp = item.GetComponent<HexTile>();
            if (temp != null)
            {
                if (!temp.Occupied)
                {
                    return temp;
                }
            }
        }

        return null;
    }

    void RefreshEnergy()
    {
        currentStats.energyPool = stats.energyPool;
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

    public bool CheckExecuting()
    {
        return executing;
    }
}