using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class GOAPAction
{
    HashSet<KeyValuePair<string, object>> preCon = new HashSet<KeyValuePair<string, object>>();
    HashSet<KeyValuePair<string, object>> effects = new HashSet<KeyValuePair<string, object>>();
    public int energyCost;
    public int priority;

    bool inRange = false;

    [NonSerialized] public DummyCharacter target;

    [NonSerialized] public GOAPAction parent;

    // ImportANT!! Use this to reset action to reset the entire action completely
    public void ResetAction()
    {
        energyCost = 0;
        target = null;
        parent = null;
        Reset();
    }

    public abstract void Reset();
    public abstract bool CheckExecuted();
    public abstract bool CheckPrecon(DummyCharacter chara);
    public abstract void Execute(DummyCharacter chara);
    public abstract bool NeedsRange();
    public abstract bool NeedsEnergy();

    public bool InRange
    {
        get { return inRange; }
        set { inRange = value; }
    }

    public void AddPrecon(string key, object value)
    {
        preCon.Add(new KeyValuePair<string, object>(key, value));
    }

    public void AddEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }

    public HashSet<KeyValuePair<string, object>> ReturnPreCon()
    {
        return preCon;
    }

    public HashSet<KeyValuePair<string, object>> ReturnEffects()
    {
        return effects;
    }
}
