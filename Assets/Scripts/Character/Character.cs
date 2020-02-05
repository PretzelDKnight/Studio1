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
    Stats stats = null;
    Stats currentStats = null;
    Status status = Status.Normal;
    bool AI = false;

    HexTile tile = null;

    public abstract void Attack();
    public abstract void SkillOne();
    public abstract void SkillTwo();

    protected void Awake()
    {

    }

    public void GetCurrentTile()
    {
        tile = GetTargetTile(this);
    }

    protected HexTile GetTargetTile(Character chara)
    {
        HexTile hTile = null;
        RaycastHit hit;
        if (Physics.Raycast(chara.gameObject.transform.position, -Vector3.up, out hit, 1))
        {
            hTile = hit.collider.GetComponent<HexTile>();
        }
        return hTile;
    }

    protected void FindSelectableTiles()
    {

    }

    protected void Move()
    {

    }

    protected void VangaurdAttack()
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
        if (currentStats.speed > chara.currentStats.speed)
            return 1;
        else if (currentStats.speed == chara.currentStats.speed)
            return 0;
        else
            return -1;
    }
}
