using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePSA : AbstractMenu
{
    // Start is called before the first frame update
    void Update()
    {
        Debug.ClearDeveloperConsole();
        Debug.LogError("Game");
        Debug.LogError("Press Q for Pause Menu");
    }

    // Update is called once per frame
    public override void Function()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PushDownAutomata.instance.AddPause();
        }
    }
}
