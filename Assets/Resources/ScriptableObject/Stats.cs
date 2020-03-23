using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public int attackRange;
    public int speed;
    public int skill1range;
    public int skill2range;
    public float statusResist;
    public float damageResist;

    public void Copy(BaseStats baseStats)
    {
        attackRange = baseStats.attackRange;
        speed = baseStats.speed;
        skill1range = baseStats.skill1Range;
        skill2range = baseStats.skill2range;
        statusResist = baseStats.statusResist;
        damageResist = baseStats.damageResist;
    }
}
