using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public float attackRange;
    public float speed;
    public float statusResist;
    public float damageResist;

    public void Copy(BaseStats baseStats)
    {
        attackRange = baseStats.attackRange;
        speed = baseStats.speed;
        statusResist = baseStats.statusResist;
        damageResist = baseStats.damageResist;
    }
}
