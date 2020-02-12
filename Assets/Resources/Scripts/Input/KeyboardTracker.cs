using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTracker : DeviceTracker
{
    public AxisKeys[] axisKeys;
    public KeyCode[] buttonKeys;

    private void Reset()
    {
        IM = GetComponent<InputManager>();
        axisKeys = new AxisKeys[IM.axisCount];
        buttonKeys = new KeyCode[IM.buttonCount];
    }

    // Update is called once per frame
    void Update()
    {
        // Check for inputs. If inputs are detected, set new data to true
        // Create and populate input data to pass to the inputmanager

        for (int i = 0; i < axisKeys.Length; i++)
        {
            float val = 0f;

            if (Input.GetKey(axisKeys[i].pos))
            {
                val += 1;
                newData = true;
            }

            if (Input.GetKey(axisKeys[i].neg))
            {
                val -= 1f;
                newData = true;
            }

            data.axes[i] = val;
        }

        for (int i = 0; i < buttonKeys.Length; i++)
        {
            if (Input.GetKey(buttonKeys[i]))
            {
                data.buttons[i] = true;
                newData = true;
            }
        }

        if (newData)
        {
            IM.PassInput(data);
            newData = false;
            data.Reset();
        }
    }

    public override void Refresh()
    {
        IM = GetComponent<InputManager>();

        // create 2 temp arrays for buttons and axis
        KeyCode[] newButtons = new KeyCode[IM.buttonCount];
        AxisKeys[] newAxes = new AxisKeys[IM.axisCount];

        if (buttonKeys != null)
        {
            for (int i = 0; i < Mathf.Min(newButtons.Length, buttonKeys.Length); i++)
            {
                newButtons[i] = buttonKeys[i];
            }
        }

        buttonKeys = newButtons;

        if (axisKeys != null)
        {
            for (int i = 0; i < Mathf.Min(newAxes.Length, axisKeys.Length); i++)
            {
                newAxes[i] = axisKeys[i];
            }
        }

        axisKeys = newAxes;
    }
}

[System.Serializable]
public struct AxisKeys
{
    public KeyCode pos;
    public KeyCode neg;
}