using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    static List<Character> allies = new List<Character>();
    static List<Character> enemies = new List<Character>();

    static Queue<Character> turnOrder = new Queue<Character>();

    static void InitTurnQueue()
    {
        List<Character> teamList = SortFastest();

        foreach (Character unit in teamList)
            turnOrder.Enqueue(unit);

        StartTurn();
    }

    static List<Character> SortFastest()
    {
        List<Character> tempList = new List<Character>(allies);

        foreach (var item in enemies)
            tempList.Add(item);

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
            InitTurnQueue();
    }

    public static IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1f);
        Character unit = turnOrder.Dequeue();
        unit.EndTurn();
        StartTurn();
        yield return null;
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
            BattleManager.instance.Battle = false;
        }

        if (enemies.Contains(unit))
            enemies.Remove(unit);

        if (enemies.Count == 0)
        {
            // Battle Win Call
            Debug.Log("You Won! You beat them into dust!!");
            BattleManager.instance.Battle = false;
        }
    }

    static public IEnumerator NewBattle(List<Character> enemyList)
    {
        turnOrder.Clear();
        allies = Player.instance.GetParty();
        enemies = enemyList;

        HexGrid.instance.GenerateHexGrid();

        foreach (var ally in allies)
        {
            ally.MoveToNearestTile();
        }
        foreach (var enemy in enemies)
        {
            enemy.MoveToNearestTile();
        }

        InitTurnQueue();
        yield return null;
    }
}