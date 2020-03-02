using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPSA : AbstractMenu
{
    // Start is called before the first frame update
    void Update()
    {
        Debug.ClearDeveloperConsole();
        Debug.LogError("Main Menu");
        Debug.LogError("Press Q for New Game");
        Debug.LogError("Press W for Help Menu");
    }

    // Update is called once per frame
    public override void Function()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PushDownAutomata.instance.AddGame();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            PushDownAutomata.instance.AddHelp();
        }
    }
}
