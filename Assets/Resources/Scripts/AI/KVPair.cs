using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KVPair
{
    public string key;
    public bool value;

    public KeyValuePair<string, object> Convert()
    {
        return new KeyValuePair<string, object>(key, value);
    }
}
