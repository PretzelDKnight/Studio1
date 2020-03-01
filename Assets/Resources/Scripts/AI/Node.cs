using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Node class for Tree generation with GOAP
public class Node
{
    public Node parent; //Reference to its parent tile
    public GOAPAction action; //Its action
    public int energy; //Its cost
    public int priority; // Priority of the action
    public HexTile targetTile = null;
    public Character target = null;
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

    public Node(Node parent, KeyValuePair<string, object> goal, GOAPAction action)
    {
        this.parent = parent;
        this.state = new HashSet<KeyValuePair<string, object>>() { goal };
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
