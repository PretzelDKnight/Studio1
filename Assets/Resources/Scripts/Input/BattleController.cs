using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : Controller
{
    // Update is called once per frame
    void LateUpdate()
    {
        if(BattleManager.Battle)
            MouseFunction();
    }

    public override void ReadInput(InputData data)
    {
        throw new System.NotImplementedException();
    }

    public void MouseFunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Tile")
            {
                HexTile temp = hit.transform.GetComponent<HexTile>();
                temp.Hovered = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (BattleManager.targetTile == null)
                    {
                        if (temp.Walkable && BattleManager.ReturnState() == State.Move)
                        {
                            BattleManager.targetTile = temp;
                            temp.SetSelected();
                            BattleManager.instance.RecievedInput();
                        }
                        else if (temp.Attackable && BattleManager.ReturnState() == State.Attack)
                        {
                            BattleManager.targetTile = temp;
                            BattleManager.targetChara = temp.ReturnChara();
                            temp.SetSelected();
                            BattleManager.instance.RecievedInput();
                        }
                    }
                }
            }
        }
    }
}
