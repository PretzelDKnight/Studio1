using System.Collections.Generic;
using UnityEngine;


// SINGLETON CLASS!
public class TurnManager : MonoBehaviour
{
    public CharacterVariable currentChara;

    public PartyVariable party;
    public PartyVariable enemies;

    static List<Character> allChara = new List<Character>();

    static Queue<Character> turnOrder = new Queue<Character>();

    public GameEvent battleLose;
    public GameEvent battleWin;
    public GameEvent battleEnd;
    public GameEvent newChara;

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
        currentChara.character = turnOrder.Dequeue();
        newChara.Raise();
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

        foreach (var member in party.members)
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

        foreach (var member in enemies.members)
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

        if (allyDead == party.members.Length)
        {
            // Game Over Call
            battleEnd.Raise();
            battleLose.Raise();
        }

        if (enemyDead == enemies.members.Length)
        {
            // Battle Win Call
            battleEnd.Raise();
            battleWin.Raise();
        }
    }

    // Starts new battle
    public void NewBattle()
    {
        turnOrder.Clear();

        allChara = new List<Character>();

        foreach (var ally in party.members)
        {
            if (ally != null)
            {
                ally.MoveToNearestTile();
                allChara.Add(ally);
            }
        }

        // Need to find way to deal with Spawning enemies through scriptable objects

        //foreach (var enemy in enemies.members)
        //{
        //    if (enemy != null)
        //    {
        //        enemy.MoveToNearestTile();
        //        allChara.Add(enemy);
        //    }
        //}

        InitTurnQueue();
    }
}