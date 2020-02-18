using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class GOAPAction : ScriptableObject
{
    public List<KVPair> effects = new List<KVPair>();
    public List<KVPair> preCon = new List<KVPair>();
    public int energyCost;
    public int priority;
    public abstract bool CheckProceduralPrecondition(Character chara);
    public abstract bool CheckAction(Character chara, out HexTile tile, out Character target);
    public abstract void Execute(Character chara, HexTile tile, Character target);
    public HashSet<KeyValuePair<string, object>> ReturnPreCon() 
    {
        HashSet<KeyValuePair<string, object>> list = new HashSet<KeyValuePair<string, object>>();

        foreach (var item in preCon)
        {
            list.Add(item.Convert());
        }
        return list; 
    }
    public HashSet<KeyValuePair<string, object>> ReturnEffects()
    {
        HashSet<KeyValuePair<string, object>> list = new HashSet<KeyValuePair<string, object>>();

        foreach (var item in effects)
        {
            list.Add(item.Convert());
        }
        return list;
    }
}
