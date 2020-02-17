using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDowns : ScriptableObject
{
    public int S1CD;
    public int S2CD;

    public void Copy(CoolDowns cd)
    {
        S1CD = cd.S1CD;
        S2CD = cd.S2CD;
    }

    public void Reset()
    {
        S1CD = 0;
        S2CD = 0;
    }
}
