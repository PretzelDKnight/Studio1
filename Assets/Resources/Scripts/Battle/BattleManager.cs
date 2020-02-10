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

    bool interaction = true;

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
        Debug.Log("Interaction : " + interaction);
        if (!busy && interaction)
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
                        if (temp.Walkable && state == State.Move)
                        {
                            targetTile = temp;
                            targetTile.SetSelected();
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
                currentChar.character.energy.runTimeValue -= targetTile.energyCost;
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    public void Attack()
    {
        state = State.Attack;
    }

    public void Skill1()
    {
        state = State.Skill1;
    }

    public void Skill2()
    {
        state = State.Skill2;
    }

    public void Move()
    {
        state = State.Move;
        Pathfinder.instance.FindSelectableTiles(currentChar.character);
    }

    public void Pass()
    {
        currentChar.character.SetNotShown();
        endChara.Raise();
    }

    public bool Battle
    {
        get { return battle; }
        set { battle = value; }
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
        currentChar.character.SetShown();
        ResetEverything();
        Move();
    }

    public void NextMove()
    {
        ResetEverything();

        if (currentChar.character.energy.runTimeValue != 0)
            Move();
        else
            Pass();
    }

    public void ResetEverything()
    {
        targetChara = null;
        targetTile = null;
        resetTiles.Raise();
        busy = false;
    }

    public void SetInteractionFalse()
    {
        interaction = false;
    }

    public void SetInteractionTrue()
    {
        interaction = true;
    }
}