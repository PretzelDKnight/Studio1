using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum State
{
    Attack,
    Skill1,
    Skill2,
    Move
}

public class BattleManager : MonoBehaviour
{
    static public BattleManager instance = null;

    public CharacterVariable currentChar;

    [SerializeField] LayerMask layerMask;
    static bool battle = false;
    bool busy = false;
    static State state;
    static HexTile targetTile;
    static Character targetChara;

    [SerializeField] public Color whenHovered = Color.white;
    [SerializeField] public Color whenTargetable = Color.white;
    [SerializeField] public Color whenSelected = Color.white;
    [SerializeField] public Color whenNormal = Color.white;

    public GameEvent resetTiles;
    public GameEvent battleStart;
    public GameEvent endChara;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        state = State.Move;
    }

    private void LateUpdate()
    {
        if (!busy)
            MouseFunction();
    }

    public void MouseFunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            if (hit.transform.tag == "Tile")
            {
                HexTile temp = hit.transform.GetComponent<HexTile>();
                temp.Hovered = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (targetTile == null)
                    {
                        //if (!temp.Occupied)
                            //if (temp.Walkable && state == State.Move)
                            {
                                targetTile = temp;
                                targetTile.SetSelected();
                                Debug.Log(targetTile.returnNeighbours().Count);
                                RecievedInput();
                            }
                    }
                }
            }
            else if (hit.transform.tag == "Enemy" && currentChar.character.tag == "Ally" && state == State.Attack)
            {

            }
        }
    }

    void RecievedInput()
    {
        busy = true;
        switch (state)
        {
            case State.Attack:
                currentChar.character.Attack(targetChara);
                break;
            case State.Move:
                StartCoroutine(currentChar.character.MoveDownPath(Pathfinder.instance.FindPath(currentChar.character.GetCurrentTile(), targetTile)));
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    static public void Attack()
    {
        state = State.Attack;
    }

    static public void Skill1()
    {
        state = State.Skill1;
    }

    static public void Skill2()
    {
        state = State.Skill2;
    }

    static public void Move()
    {
        state = State.Move;
    }

    public void Pass()
    {
        endChara.Raise();
    }

    public bool Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    static void ResetTargets()
    {
        targetChara = null;
        targetTile = null;
    }

    public void StartBattle()
    {
        if (!Battle)
        {
            Battle = true;
            busy = true;
            // Insert Event to Start Battle!
            battleStart.Raise();
        }
        else
            Debug.Log("Battle already running!");
    }

    public void NewChara()
    {
        currentChar.character.RefreshEnergy();
        ResetEverything();
        // Insert event to reset Hexgrid tiles and make target tiles and chara null
        Debug.Log("We Moving BRUH!");
    }

    public void NextMove()
    {
        ResetEverything();
    }

    public void ResetEverything()
    {
        targetChara = null;
        targetTile = null;
        resetTiles.Raise();
        busy = false;
    }
}