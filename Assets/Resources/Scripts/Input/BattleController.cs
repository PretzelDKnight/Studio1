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
                        else if (BattleManager.ReturnState() == State.Attack)
                        {
                            if (temp.Attackable)
                            {
                                BattleManager.targetTile = temp;
                                BattleManager.targetChara = temp.occupant;
                                temp.SetSelected();
                                BattleManager.instance.RecievedInput();
                            }                            
                        }
                        else if (BattleManager.ReturnState() == State.Skill1)
                        {
                            if (temp.Attackable)
                            {
                                BattleManager.targetTile = temp;
                                BattleManager.targetChara = temp.occupant;
                                temp.SetSelected();
                                BattleManager.instance.RecievedInput();
                            }
                        }
                        else if (BattleManager.ReturnState() == State.Skill2)
                        {
                            if (temp.Attackable)
                            {
                                BattleManager.targetTile = temp;
                                BattleManager.targetChara = temp.occupant;
                                temp.SetSelected();
                                BattleManager.instance.RecievedInput();
                            }
                        }
                        else if (BattleManager.instance.currentChar.GetType() == typeof(Gunner) && temp.Hovered && temp.Occupied && BattleManager.ReturnState() == State.Skill1)
                        {
                            FindTilesInSight(BattleManager.instance.currentChar, temp);
                            BattleManager.targetTile = temp;
                            BattleManager.targetChara = temp.occupant;
                            temp.SetSelected();
                            BattleManager.instance.RecievedInput();
                        }
                        else if (BattleManager.instance.currentChar.GetType() == typeof(Gunner) && temp.Hovered && temp.Occupied && BattleManager.ReturnState() == State.Skill2)
                        {
                            FindTilesInSight(BattleManager.instance.currentChar, temp);
                            BattleManager.targetTile = temp;
                            BattleManager.targetChara = temp.occupant;
                            temp.SetSelected();
                            BattleManager.instance.RecievedInput();
                        }
                    }
                }
            }
        }
    }

    public void FindTilesInSight(Character source, HexTile tile)
    {
        bool temp = false;

        RaycastHit[] hit;

        float distToSelectedTile = Vector3.Distance(source.GetCurrentTile().transform.position, tile.transform.position);

        hit = Physics.RaycastAll(source.GetCurrentTile().transform.position, tile.transform.position, distToSelectedTile, TileManager.instance.layerMask);

        //This code loops through all the tiles in a straight line until it finds an enemy in sight and sets them to attackable
        //--------------------------------------------
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.GetComponent<HexTile>().occupant != null && temp == false)
            {
                hit[i].collider.gameObject.GetComponent<HexTile>().Attackable = true;
                temp = true;
            }
            else if (temp == true)
                break;
        }
        //--------------------------------------------
    }
    
    public void CalcDropOff(Gunner source, float dist)
    {
        source.dropoffval = dist / 2;
    }
}
