using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;

    HashSet<KeyValuePair<string, object>> AIGoals;

    void Start()
    {
        
    }

    void AddGoal()
    {
        AIGoals.Add(new KeyValuePair <string, object>("kAttack", false));
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public Queue<GOAPAction> plan(DummyCharacter character, HashSet<GOAPAction> availableActions, HashSet<KeyValuePair<string, object>> goal)
    {
        // Reset Actions
        foreach (GOAPAction a in availableActions)
        {
            a.ResetAction();
        }
        
        // Check the usable actions based on their PreCons
        HashSet<GOAPAction> usableActions = new HashSet<GOAPAction>();
        foreach (GOAPAction a in availableActions)
        {
            if (a.CheckProceduralPrecon(character))
                usableActions.Add(a);
        }

        
        //Creates our tiles that result in a solution to the goal
        List<Tile> tiles = new List<Tile>();

        // Graph building
        Tile start = new Tile(null, 0, null, null);
        bool success = graphBuild(start, tiles, usableActions, goal);

        if (!success)
        {
            Debug.Log("No viable plan!");
            return null;
        }

        //Finding the cheapest tile
        Tile cheapest = null;
        foreach (Tile tile in tiles)
        {
            if (cheapest == null)
                cheapest = tile;
            else
            {
                if (tile.cost < cheapest.cost)
                    cheapest = tile;
            }
        }

        //Backtracking along the parents, just like in A*
        List<GOAPAction> resultList = new List<GOAPAction>();
        Tile t = cheapest;
        while (t != null)
        {
            if (t.action != null)
            {
                resultList.Insert(0, t.action);
            }
            t = t.parent;
        }

        //Returning the final plan
        Queue<GOAPAction> queue = new Queue<GOAPAction>();
        foreach (GOAPAction a in resultList)
        {
            queue.Enqueue(a);
        }

        return queue;
    }

    bool graphBuild(Tile parent, List<Tile> tiles, HashSet<GOAPAction> usableActions, HashSet<KeyValuePair<string, object>> goal)
    {
        bool foundOne = false;

        foreach (GOAPAction action in usableActions)
        {
            if (inState(action.ReturnPreCon(), parent.state))
            {
                //Apply action to parent
                HashSet<KeyValuePair<string, object>> currentState = populateState(parent.state, action.ReturnEffects());
                Tile tile = new Tile(parent, parent.cost + action.energyCost, currentState, action);

                if (goalInState(goal, currentState))
                {
                    tiles.Add(tile);
                    foundOne = true;
                }
                else
                {
                    //Checking the remaining actions
                    HashSet<GOAPAction> subset = actionSubset(usableActions, action);
                    bool found = graphBuild(tile, tiles, subset, goal);
                    if (found)
                        foundOne = true;
                }
            }
        }

        return foundOne;
    }
    protected HashSet<GOAPAction> actionSubset(HashSet<GOAPAction> actions, GOAPAction removeMe)
    {
        HashSet<GOAPAction> subset = new HashSet<GOAPAction>();
        foreach (GOAPAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

    protected bool goalInState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
    {
        bool match = false;
        foreach (KeyValuePair<string, object> t in test)
        {
            foreach (KeyValuePair<string, object> s in state)
            {
                if (s.Equals(t))
                {
                    match = true;
                    break;
                }
            }
        }
        return match;
    }

    protected bool inState(HashSet<KeyValuePair<string, object>> test, HashSet<KeyValuePair<string, object>> state)
    {
        bool allMatch = true;
        foreach (KeyValuePair<string, object> t in test)
        {
            bool match = false;
            foreach (KeyValuePair<string, object> s in state)
            {
                if (s.Equals(t))
                {
                    match = true;
                    break;
                }
            }
            if (!match)
                allMatch = false;
        }
        return allMatch;
    }
    protected HashSet<KeyValuePair<string, object>> populateState(HashSet<KeyValuePair<string, object>> currentState, HashSet<KeyValuePair<string, object>> stateChange)
    {
        HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();
        // copy the KVPs over as new objects
        foreach (KeyValuePair<string, object> s in currentState)
        {
            state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
        }

        foreach (KeyValuePair<string, object> change in stateChange)
        {
            // If in state update
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
                state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
                KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
                state.Add(updated);
            }
            else
            {
                // If not in state, add
                state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }
        }
        return state;
    }

}

public class Tile
{
    public Tile parent;
    public GOAPAction action;
    public float cost;
    public HashSet<KeyValuePair<string, object>> state;

    public Tile(Tile parent, float cost, HashSet<KeyValuePair<string,object>> state, GOAPAction action)
    {
        this.parent = parent;
        this.action = action;
        this.cost = cost;
        this.state = state;
    }
}