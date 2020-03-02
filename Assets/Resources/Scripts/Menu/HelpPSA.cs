using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPSA : AbstractMenu
{
    // Start is called before the first frame update
    void Update()
    {
        Debug.ClearDeveloperConsole();
        Debug.LogError("Help Menu");
        Debug.LogError("Press Esc to go back");
    }

    // Update is called once per frame
    public override void Function()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushDownAutomata.instance.PrevMenu();
        }
    }
}
