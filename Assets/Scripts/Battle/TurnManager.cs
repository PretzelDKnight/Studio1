using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    // Event Related variables
    public delegate void ClickAction();
    public static event ClickAction Function;

    bool transition = false;

    static List<Character> allies;
    static List<Character> enemies;

    static Queue<Character> turnOrder;

    // Start is called before the first frame update
    void Start()
    {
        turnOrder.Clear();
        allies = Player.instance.GetParty();
        // Place Function to retrieve enemy list
        Debug.Log("Need to load enemies list");

        // Place Function to Make Characters move to nearest tile!!!
        Debug.Log("Characters run to nearest tile");
    }

    // Update is called once per frame
    void Update()
    {
        if (turnOrder.Count == 0)
        {
            InitTurnQueue();
        }
    }

    static void InitTurnQueue()
    {
        List<Character> teamList = SortFastest();

        foreach (Character unit in teamList)
        {
            turnOrder.Enqueue(unit);
        }

        StartTurn();
    }

    static List<Character> SortFastest()
    {
        List<Character> tempList = new List<Character>(allies);

        foreach (var item in enemies)
        {
            tempList.Add(item);
        }

        tempList.Sort();

        return tempList;
    }

    static void StartTurn()
    {
        if (turnOrder.Count > 0)
        {
            Character unit = turnOrder.Peek();
            unit.BeginTurn();
        }
        else
        {
            InitTurnQueue();
        }
    }

    public static void EndTurn()
    {
        Character unit = turnOrder.Dequeue();
        unit.EndTurn();
        StartTurn();
    }

    public static void DeleteFromQueue(Character unit)
    {
        List<Character> list = new List<Character>();

        if (turnOrder.Contains(unit))
        {
            foreach (var item in turnOrder)
            {
                if (item != unit)
                    list.Add(turnOrder.Dequeue());
                else
                    turnOrder.Dequeue();
            }

            turnOrder.Clear();

            foreach (var item in list)
            {
                turnOrder.Enqueue(item);
            }
        }

        if (allies.Contains(unit))
            allies.Remove(unit);

        if (allies.Count ==0)
        {
            // Game Over Call
            Debug.Log("Game Over! You lost the fight! BooHoo!");
        }

        if (enemies.Contains(unit))
            enemies.Remove(unit);

        if (enemies.Count == 0)
        {
            // Battle Win Call
            Debug.Log("You Won! You beat them into dust!!");
        }
    }
}