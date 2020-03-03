using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePSA : AbstractMenu
{
    // Start is called before the first frame update
    void Message()
    {
        Debug.ClearDeveloperConsole();
        Debug.LogError("Pause Menu");
        Debug.LogError("Press Esc to return to Game");
        Debug.LogError("Press Q for Help Menu");
        Debug.LogError("Press W to exit to Main Menu");
    }

    // Update is called once per frame
    public override void Function()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushDownAutomata.instance.PrevMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PushDownAutomata.instance.AddHelp();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            PushDownAutomata.instance.ClearStack();
        }
    }
}
