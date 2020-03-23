using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseStats : ScriptableObject
{
    public int energy;
    public float health;
    public int attackRange;
    public int skill1Range;
    public int skill2range;
    public int speed;
    public float statusResist;
    public float damageResist;
}
