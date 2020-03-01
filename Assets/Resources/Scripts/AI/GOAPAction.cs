using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class GOAPAction : ScriptableObject
{
    public KVPair effects = new KVPair();
    public KVPair preCon = new KVPair();
    public int energyCost;
    public int priority;
    public abstract bool CheckProceduralPrecondition(Character chara);
    public abstract bool CheckAction(Character chara, out HexTile tile, out Character target);
    public abstract void Execute(Character chara, HexTile tile, Character target);
    public KeyValuePair<string, object> ReturnPreCon() 
    {
        return preCon.Convert();
    }
    public KeyValuePair<string, object> ReturnEffects()
    {
        return effects.Convert();
    }
}
