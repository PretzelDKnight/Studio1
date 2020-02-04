using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public abstract class DeviceTracker : MonoBehaviour
{
    protected InputManager IM;
    protected InputData data;
    protected bool newData; // flag

    private void Awake()
    {
        IM = GetComponent<InputManager>();
        data = new InputData(IM.axisCount, IM.buttonCount);
    }

    public abstract void Refresh();
}
