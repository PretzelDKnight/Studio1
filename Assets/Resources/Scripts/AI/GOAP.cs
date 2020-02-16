using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;
    static HexTile ghostTile = null;
    static Character ghostTarget = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Queue<GOAPAction> GOAPlan (Character source, HashSet<KeyValuePair<string, object>> goal)
    {
        List<Node> leaves = new List<Node>();

        HashSet<GOAPAction> actions = new HashSet<GOAPAction>();
        foreach (var action in source.actions)
        {
            actions.Add(action);
        }

        Node start = new Node(null, goal, null);
        bool reachedGoal = GrowTree(start, leaves, actions, goal);

        if (!reachedGoal)
        {
            Debug.Log("No viable plan of action retrieved");
            return null;
        }

        // Return action with highest priority but within managable energy cost
        Node highestP = null;
        int highest = 0;
        foreach (var leaf in leaves)
        {
            if (highestP == null)
                highestP = leaf;
            else
            {
                if (leaf.priority > highest && leaf.energy <= source.energy.runTimeValue)
                {
                    highestP = leaf;
                    highest = leaf.priority;
                }
            }
        }

        // Going through parents and adding to queue
        Queue<GOAPAction> queue = new Queue<GOAPAction>();
        Node temp = highestP;
        while (temp != null)
        {
            if (temp.action != null)
                queue.Enqueue(temp.action);
            temp = temp.parent;
        }

        return queue;
    }

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
    static bool GrowTree(Node parent, List<Node> leaves, HashSet<GOAPAction> actions, HashSet<KeyValuePair<string, object>> goal)
    {
        bool pathFound = false;

        foreach (var action in actions)
        {
            if (InState(action.ReturnPreCon(), goal))
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
                    bool found = GrowTree(node, leaves, subset, goal);
                    if (found)
                        pathFound = true;
                }
            }
        }
        return pathFound;
    }

    // GOAP function to check for enemy within attack range
    static public bool EnemyInRange(Character source)
    {
        bool check = false; ;
        int range = source.stats.attackRange;
        List<HexTile> tempList = new List<HexTile>();

        if (ghostTile != null)
            tempList.Add(ghostTile);
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
                            ghostTarget = tempChara;
                            return true;
                        }
                    }
                }
            }
        }

        ResetTiles(tempList);
        return check;
    }

    // Function to set ghost tile for further Procedural condition checking for GOAP
    static public bool MoveCheck(Character source)
    {
        List<HexTile> list = NextSelectableTiles(source);

        foreach (var tile in list)
        {
            if (EnemyInRange(source))
            {
                ghostTile = tile;
                return true;
            }
        }

        // if no tile is directly in attackable range
        ghostTarget = NearestTarget(source);
        HexTile temp = list[0];
        float lowest = Vector3.Distance(list[0].transform.position, ghostTarget.transform.position);
        
        foreach (var tile in list)
        {
            float dist = Vector3.Distance(tile.transform.position, ghostTarget.transform.position);
            if (lowest > dist)
            {
                lowest = dist;
                temp = tile;
            }
        }
        ghostTile = temp;

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

    static public void ResetGOAP()
    {
        ghostTile = null;
        ghostTarget = null;
    }

    static public HexTile ReturnTile()
    {
        return ghostTile;
    }

    static public Character ReturnTarget()
    {
        return ghostTarget;
    }

    // Node class for A* with GOAP
    public class Node
    {
        public Node parent; //Reference to its parent tile
        public GOAPAction action; //Its action
        public int energy; //Its cost
        public int priority; // Priority of the action
        public HashSet<KeyValuePair<string, object>> state; // Its Precondition to retrieve children

        public Node(Node parent, HashSet<KeyValuePair<string, object>> state, GOAPAction action)
        {
            this.parent = parent;
            this.state = state;
            this.action = action;

            if (parent != null)
            {
                this.priority = action.priority + parent.priority;
                this.energy = action.energyCost + parent.priority;
            }
            else
            {
                priority = 0;
                energy = 0;
            }
        }
    }
}