using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    [SerializeField] public float attack;
    [SerializeField] public float defence;
    [SerializeField] public float speed;
    [SerializeField] public float resist;
    [SerializeField] public float range;
    [SerializeField] public int energyPool;
}
