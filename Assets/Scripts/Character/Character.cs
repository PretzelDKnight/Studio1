using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    Normal,
    Stunned,
    Staggered
}

public abstract class Character : MonoBehaviour
{
    Stats stats = null;
    Status status = Status.Normal;
    bool AI = false;

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
}
