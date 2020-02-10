using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    [NonSerialized] public float runTimeValue;

    public void Copy(float value)
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
