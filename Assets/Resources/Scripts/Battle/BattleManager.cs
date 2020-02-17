﻿using UnityEngine;
using System;
using System.Collections.Generic;

public enum State
{
    Attack,
    Skill1,
    Skill2,
    Move
}

// SINGLETON CLASS!
public class BattleManager : MonoBehaviour
{
    static public BattleManager instance = null;

    public CharacterVariable currentChar;

    [SerializeField] LayerMask layerMask;
    static bool battle = false;
    bool busy = false;
    static State state;
    static public HexTile targetTile;
    static public Character targetChara;

    [SerializeField] public Color whenHovered = Color.white;
    [SerializeField] public Color whenTargetable = Color.white;
    [SerializeField] public Color whenSelected = Color.white;
    [SerializeField] public Color whenNormal = Color.white;

    public GameEvent resetTiles;

    bool interaction = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        state = State.Move;
    }

    // Executes the function depending on state of the battle manager
    public void RecievedInput()
    {
        busy = true;
        switch (state)
        {
            case State.Attack:
                currentChar.character.Attack(targetChara);
                break;
            case State.Move:
                currentChar.character.Move(targetTile);
                break;
            case State.Skill1:
                break;
            case State.Skill2:
                break;
            default:
                break;
        }
    }

    // Attack function to change state of battle manager functions
    public void Attack()
    {
        ResetEverything();
        state = State.Attack;
        Pathfinder.instance.FindTilesWithinRange(currentChar.character);
    }

    // Skill 1 Function to change state of battle manager functions
    public void Skill1()
    {
        ResetEverything();
        state = State.Skill1;
    }

    // Skill 2 Function to change state of battle manager functions
    public void Skill2()
    {
        ResetEverything();
        state = State.Skill2;
    }

    // Move function to change state of battle manager functions
    public void Move()
    {
        ResetEverything();
        state = State.Move;
        Pathfinder.instance.FindSelectableTiles(currentChar.character);
    }

    // Pass to send the turn to the next character in queue
    public void Pass()
    {
        currentChar.character.SetNotShown();
        TurnManager.instance.EndTurn();
    }

    // Get set battle function
    static public bool Battle
    {
        get { return battle; }
        set { battle = value; }
    }

    // Starts the battle after event is raised
    public void StartBattle()
    {
        if (!Battle)
        {
            Battle = true;
            busy = true;
            HexGrid.instance.GenerateHexGrid();
            TurnManager.instance.NewBattle();
        }
        else
            Debug.Log("Battle already running!");
    }

    // Is raised when a new character enters the Battle manager
    public void NewChara()
    {
        currentChar.character.RefreshEnergy();
        currentChar.character.SetShown();
        // TODO : Add in functionality to change card material depending on character
        HandCards.instance.GenerateHand();
        TurnCards.instance.GenerateStatCards();
        ResetEverything();
        if (!currentChar.character.AI)
        {
            HandCards.instance.SetHandMove();
            Move();
        }
        else
        {
            AIFunction();
        }
    }

    // If the same character has energy left and is making a move after having done a move, an event raises this function
    public void NextMove()
    {
        ResetEverything();

        if (currentChar.character.energy.runTimeValue != 0)
        {
            HandCards.instance.SetHandMove();
            Move();
        }
        else
            Pass();
    }

    public void AIFunction()
    {
        Queue<Node> queue = GOAP.GOAPlan(currentChar.character, ConvertGoals());
        foreach (var node in queue)
        {
            queue.Dequeue().action.Execute(currentChar.character, node.targetTile, node.target);
        }
    }

    private HashSet<KeyValuePair<string, object>> ConvertGoals()
    {
        HashSet<KeyValuePair<string, object>> aIGoals = new HashSet<KeyValuePair<string, object>>();
        foreach (var goal in currentChar.character.goals)
        {
            aIGoals.Add(goal);
        }

        return aIGoals;
    }

    // Resets the all targeting values to null and raises events
    public void ResetEverything()
    {
        targetChara = null;
        targetTile = null;
        resetTiles.Raise();
        busy = false;
    }

    // Sets interaction with battle manager to false when story or other events are raised in battle
    public void SetInteractionFalse()
    {
        interaction = false;
    }

    // Sets interaction with battle manager back to true after said story
    public void SetInteractionTrue()
    {
        interaction = true;
    }

    static public State ReturnState()
    {
        return state;
    }

    public void EndBattle()
    {
        Battle = false;
    }
}