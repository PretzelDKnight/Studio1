using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    [SerializeField] float attack;
    [SerializeField] float defence;
    [SerializeField] float speed;
    [SerializeField] float resist;
    [SerializeField] int energyPool;
}
