using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseStats : ScriptableObject
{
    public int energy;
    public float health;
    public float attackRange;
    public float speed;
    public float statusResist;
    public float damageResist;
}
