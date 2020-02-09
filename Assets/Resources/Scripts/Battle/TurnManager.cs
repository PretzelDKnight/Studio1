using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    void InitTurnQueue()
    {
        //HealthCheck();
        allChara.Sort();

        if (turnOrder.Count == 0)
            foreach (Character unit in allChara)
                turnOrder.Enqueue(unit);

        StartTurn();
    }

    void StartTurn()
    {
        currentChara.character = turnOrder.Dequeue();
        currentChara.TurnStart();
        newChara.Raise();
    }

    public void EndTurn()
    {
        currentChara.TurnEnd();
        InitTurnQueue();
    }

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