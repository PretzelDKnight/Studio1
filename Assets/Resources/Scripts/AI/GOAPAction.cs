using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class GOAPAction : ScriptableObject
{
    public HashSet<KeyValuePair<string, object>> preCon = new HashSet<KeyValuePair<string, object>>();
    public HashSet<KeyValuePair<string, object>> effects = new HashSet<KeyValuePair<string, object>>();
    public int energyCost;
    public int priority;
    public bool InRange;
    public abstract bool CheckProceduralPrecon(Character chara);
    public abstract void Execute(Character chara);
    public HashSet<KeyValuePair<string, object>> ReturnPreCon() { return preCon; }
    public HashSet<KeyValuePair<string, object>> ReturnEffects() { return effects; }
}
