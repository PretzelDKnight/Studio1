﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    static public Queue<Node> GOAPlan (Character source, HashSet<KeyValuePair<string, object>> goal)
    {
        List<Node> leaves = new List<Node>();

        HashSet<GOAPAction> actions = new HashSet<GOAPAction>();
        foreach (var action in source.actions)
        {
            actions.Add(action);
        }

        Node start = new Node(null, goal, null);
        bool reachedGoal = GrowGraph(start, leaves, actions, goal);

        if (!reachedGoal)
        {
            Debug.Log("No viable plan of action retrieved");
            return null;
        }

        // Return action with highest priority but within managable energy cost
        Node highestP = null;
        int highest = -1;
        foreach (var leaf in leaves)
        {
            if (leaf.priority >= highest)
            {
                highestP = leaf;
                highest = leaf.priority;
            }
        }

        if (highestP == null)
        {
            Debug.Log("Plan created, but not to energy specification");
            return null;
        }

        // Going through parents and adding to queue
        Queue<Node> queue = new Queue<Node>();
        Node temp = highestP;
        while (temp != null)
        {
            queue.Enqueue(temp);
            temp = temp.parent;
        }
        return queue;
    }

    //=============================================================================================== W I P ===============================================================================================
    // Function to check if path is possible depending on Action checks
    static bool CheckPlan(Character source, Node leaf)
    {
        Node temp = leaf;
        HexTile tile;
        Character target;

        while (temp != null)
        {
            if (temp.action != null)
            {
                if (temp.action.CheckAction(source, out tile, out target))
                {
                    temp.target = target;
                    temp.targetTile = tile;
                }
                else
                {
                    return false;
                }
            }
            temp = temp.parent;
        }

        return true;
    }
    //=============================================================================================== W I P ===============================================================================================

    static List<HexTile> NextSelectableTiles(Character source)
    {
        HexTile start = source.GetCurrentTile();
        float energy = source.energy.runTimeValue - source.AttackEnergy();
        List<HexTile> tempList = new List<HexTile>() { start };

        while (tempList.Count > 0)
        {
            HexTile tile = GetLowest(tempList);

            tempList.Remove(tile);

            foreach (var item in tile.ReturnNeighbours())
            {
                if (item.energyCost == 0)
                {
                    item.energyCost = source.MoveEnergy() + tile.energyCost;
                    if (item.energyCost <= energy && !item.Occupied)
                    {
                        tempList.Add(item);
                    }
                }
            }
        }

        start.ResetTileValues();

        return tempList;
    }

    static Character NearestTarget(Character source)
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("Ally");
        GameObject target =  tempList[0];
        float lowest = Vector3.Distance(source.transform.position ,tempList[0].transform.position);
        foreach (var item in tempList)
        {
            float dist = Vector3.Distance(source.transform.position, item.transform.position);
            if (lowest > dist)
            {
                lowest = dist;
                target = item;
            }
        }
        return target.GetComponent<Character>();
    }

    // GOAP function to generate tree of possible actions using recursion
    static bool GrowGraph(Node parent, List<Node> leaves, HashSet<GOAPAction> actions, HashSet<KeyValuePair<string, object>> goal)
    {
        bool pathFound = false;

        foreach (var action in actions)
        {
            if (InState(action.ReturnPreCon(), parent.state))
            {
                HashSet<KeyValuePair<string, object>> currentState = PopulateState(parent.state, action.ReturnEffects());

                Node node = new Node(parent, currentState, action);

                if (GoalInState(goal, currentState))
                {
                    leaves.Add(node);
                    pathFound = true;
                }
                else
                {
                    HashSet<GOAPAction> subset = RemoveAction(actions, action);
                    bool found = GrowGraph(node, leaves, subset, goal);
                    if (found)
                        pathFound = true;
                }
            }
        }
        return pathFound;
    }

    // GOAP function to check for enemy within attack range
    static public bool EnemyInRange(Character source, HexTile tile, out Character target)
    {
        int range = source.stats.attackRange;
        List<HexTile> tempList = new List<HexTile>();

        if (tile != null)
            tempList.Add(tile);
        else
            tempList.Add(source.GetCurrentTile());

        while (tempList.Count > 0)
        {
            HexTile temp = GetLowest(tempList);

            tempList.Remove(temp);

            foreach (var item in temp.ReturnNeighbours())
            {
                if (item.energyCost == 0)
                {
                    item.energyCost = 1 + temp.energyCost;
                    if (item.energyCost <= range)
                    {
                        tempList.Add(item);
                        Character tempChara = item.ReturnTarget(source);
                        if (tempChara != null)
                        {
                            ResetTiles(tempList);
                            target = tempChara;
                            Debug.Log("Found enemeh eheheheheheheheheheheh");
                            return true;
                        }
                        else
                            Debug.Log("Nuuuuuuuuuuu");
                    }
                }
            }
        }

        ResetTiles(tempList);
        target = null;
        return false;
    }

    // Function to set ghost tile for further Procedural condition checking for GOAP
    static public bool MoveAttackCheck(Character source, out HexTile tile, out Character target)
    {
        List<HexTile> list = NextSelectableTiles(source);
        target = null;
        tile = null;

        foreach (var item in list)
        {
            if (EnemyInRange(source, item, out target))
            {
                tile = item;
                return true;
            }
        }

        // if no tile is directly in attackable range
        return false;
    }

    // Function to return closest tile to the target
    static public bool MoveCheck(Character source, out HexTile tile, out Character target)
    {
        List<HexTile> list = NextSelectableTiles(source);
        target = NearestTarget(source);
        HexTile temp = list[0];
        float lowest = Vector3.Distance(list[0].transform.position, target.transform.position);

        foreach (var item in list)
        {
            float dist = Vector3.Distance(item.transform.position, target.transform.position);
            if (lowest > dist)
            {
                lowest = dist;
                temp = item;
            }
        }
        tile = temp;
        return true;
    }

    // Matches two Hashsets of KeyValue pairs and returns true if they all match
    static bool InState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string,object>> state)
    {
        foreach (var t in test)
        {
            foreach (var s in state)
            {
                if (!s.Equals(t))
                    return false;
            }
        }
        return true;
    }

    // Checks if atleast one goal is met
    static bool GoalInState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> goal)
    {
        foreach (var t in test)
        {
            foreach (var g in goal)
            {
                if (g.Equals(t))
                    return true;
            }
        }
        return false;
    }

    // Removes a action from a Hashset of actions
    static HashSet<GOAPAction> RemoveAction(HashSet<GOAPAction> actions, GOAPAction remove)
    {
        HashSet<GOAPAction> subset = new HashSet<GOAPAction>();
        foreach (var action in actions)
        {
            if (!action.Equals(remove))
                subset.Add(action);
        }
        return subset;
    }

    // Adds state changes to currentState and at the same time, adds a state if it doesnt exist
    static HashSet<KeyValuePair<string, object>> PopulateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
    {
        HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();

        foreach (KeyValuePair<string, object> s in currentState)
        {
            state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
        }

        foreach (KeyValuePair<string, object> change in stateChange)
        {
            // if the key exists in the current state, update the Value
            bool exists = false;

            foreach (KeyValuePair<string, object> s in state)
            {
                if (s.Key.Equals(change.Key))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                // Delegate function to return key where it equals the change state key
                state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
                KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
                Debug.Log("Hey Im working!!");
                Debug.Log(updated.ToString());
                state.Add(updated);
            }
            // if it does not exist in the current state, add it
            else
            {
                state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }
        }
        return state;
    }

    // Returns tile with the lowest cost
    static HexTile GetLowest(List<HexTile> list)
    {
        HexTile lowest = list[0];
        foreach (var item in list)
        {
            if (item.energyCost < lowest.energyCost)
                lowest = item;
        }

        return lowest;
    }

    // Resets tiles in a list
    static void ResetTiles(List<HexTile> tiles)
    {
        foreach (var tile in tiles)
        {
            tile.ResetTileValues();
        }
    }
}