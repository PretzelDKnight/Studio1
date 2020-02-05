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
    Status status = Status.Normal;
    bool AI = false;

    HexTile tile = null;

    public abstract void Attack();
    public abstract void SkillOne();
    public abstract void SkillTwo();


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
        if (stats.speed > chara.stats.speed)
            return 1;
        else if (stats.speed == chara.stats.speed)
            return 0;
        else
            return -1;
    }
}
