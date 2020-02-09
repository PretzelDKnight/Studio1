using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public int initialValue;
    [NonSerialized] public int runTimeValue;

    public void Copy(int value)
    {
        initialValue = value;
        runTimeValue = value;
    }

    public void OnAfterDeserialize()
    {
        runTimeValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
