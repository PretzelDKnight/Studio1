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
    //=============================================================== Battle Manager Variables ===============================================================
    static public BattleManager instance = null;

    public Character currentChar;

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

    //=============================================================== TurnQueue Variables ===============================================================

    public List<Character> nemenemies;

    public Party allies;
    public Party enemies;

    static List<Character> allChara = new List<Character>();

    static Queue<Character> turnOrder = new Queue<Character>();

    //=============================================================== Battle Manager Functions ===============================================================

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
        if (!currentChar.AI)
        {
            busy = true;
            switch (state)
            {
                case State.Attack:
                    currentChar.Attack(targetChara);
                    break;
                case State.Move:
                    currentChar.Move(targetTile);
                    break;
                case State.Skill1:
                    break;
                case State.Skill2:
                    break;
                default:
                    break;
            }
        }
    }

    // Attack function to change state of battle manager functions
    public void Attack()
    {
        ResetEverything();
        state = State.Attack;
        TileManager.instance.FindTilesWithinRange(currentChar);
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
        TileManager.instance.FindSelectableTiles(currentChar);
    }

    // Pass to send the turn to the next character in queue
    public void Pass()
    {
        currentChar.SetNotShown();
        EndTurn();
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
            TileManager.instance.GenerateHexGrid();
            NewBattle();
        }
        else
            Debug.Log("Battle already running!");
    }

    // If the same character has energy left and is making a move after having done a move, an event raises this function
    public void NextMove()
    {
        ResetEverything();

        if (currentChar.energy.runTimeValue != 0)
        {
            //HandCards.instance.SetHandMove();
            Move();
        }
        else
            Pass();
    }

    public void AIFunction()
    {
        Queue<Node> queue = GOAP.GOAPlan(currentChar, ConvertGoals());
        for(int i = 0; i < queue.Count; i++)
        {
            Node node = queue.Dequeue();
            node.action.Execute(currentChar, node.targetTile, node.target);
        }
    }

    private HashSet<KeyValuePair<string, object>> ConvertGoals()
    {
        HashSet<KeyValuePair<string, object>> aIGoals = new HashSet<KeyValuePair<string, object>>();
        foreach (var goal in currentChar.goals)
        {
            aIGoals.Add(goal.Convert());
        }

        return aIGoals;
    }

    // Resets the all targeting values to null and raises events
    public void ResetEverything()
    {
        targetChara = null;
        targetTile = null;
        TileManager.instance.ResetTiles();
        busy = false;
    }

    static public State ReturnState()
    {
        return state;
    }

    public void EndBattle()
    {
        Battle = false;
        TileManager.instance.DestroyGrid();
        TurnCards.instance.DestroyStatCards();
        turnOrder = new Queue<Character>();
        StartCoroutine(CameraScript.instance.ChangeCurrent(Player.instance.protagonist));
    }


    //=============================================================== TurnQueue Functions ===============================================================

    // Re-initializes turn queue and reorders characters 
    void InitTurnQueue()
    {
        //HealthCheck();
        allChara.Sort();

        if (turnOrder.Count == 0)
            foreach (Character unit in allChara)
                turnOrder.Enqueue(unit);

        StartTurn();
    }

    // Starts turn phase for the next character
    void StartTurn()
    {
        currentChar = turnOrder.Dequeue();
        currentChar.RefreshEnergy();
        currentChar.SetShown();
        StartCoroutine(CameraScript.instance.ChangeCurrent(currentChar));
        // TODO : Add in functionality to change card material depending on character
        //HandCards.instance.GenerateHand();
        TurnCards.instance.GenerateStatCards();
        ResetEverything();

        if (!currentChar.AI)
        {
            //HandCards.instance.SetHandMove();
            Move();
        }
        else
        {
            AIFunction();
        }
    }

    // Ends the turn and re-initiates queue check
    public void EndTurn()
    {
        InitTurnQueue();
    }

    // Checks health of relevant characters and checks if the party or enemies are wiped and calls battle to an end
    void HealthCheck()
    {
        int allyDead = 0;
        int enemyDead = 0;

        foreach (var member in allies.Members())
        {
            if (member != null)
            {
                if (member.health.runTimeValue <= 0)
                {
                    allyDead++;
                    if (allChara.Contains(member))
                    {
                        allChara.Remove(member);
                    }
                }
                else
                {
                    if (!allChara.Contains(member))
                    {
                        allChara.Add(member);
                    }
                }
            }
            else
            {
                allyDead++;
            }
        }

        foreach (var member in enemies.Members())
        {
            if (member != null)
            {
                if (member.health.runTimeValue <= 0)
                {
                    enemyDead++;
                    if (allChara.Contains(member))
                    {
                        allChara.Remove(member);
                    }
                }
                else
                {
                    if (!allChara.Contains(member))
                    {
                        allChara.Add(member);
                    }
                }
            }
            else
            {
                enemyDead++;
            }
        }

        if (allyDead == allies.Members().Length)
        {
            // Game Over Call
        }

        if (enemyDead == enemies.Members().Length)
        {
            // Battle Win Call
        }
    }

    // Starts new battle
    public void NewBattle()
    {
        turnOrder.Clear();

        allChara = new List<Character>();

        allies = Player.instance.allies;

        foreach (var ally in allies.Members())
        {
            if (ally != null)
            {
                ally.MoveToNearestTile();
                allChara.Add(ally);
            }
        }

        // Need to find way to deal with Spawning enemies through scriptable objects

        foreach (var enemy in enemies.Members())
        {
            if (enemy != null)
            {
                enemy.MoveToNearestTile();
                allChara.Add(enemy);
            }
        }

        InitTurnQueue();
    }
}