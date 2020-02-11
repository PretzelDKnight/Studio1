using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{
    public string action;
    public string effect;
    public string preCon;
    public int energyUsed;
    public int energyLeft;
    public int priority;

    public bool CD = false;
    
    public Action parent;

    public void ResetActions()
    {
        energyLeft = 0;
        energyUsed = 0;
        parent = null;
    }
}

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;
    
    [SerializeField] List<Action> goals;

    [SerializeField] List<Action> actionsList = new List<Action>();
    
    Dictionary<string, List<Action>> actionsDict = new Dictionary<string, List<Action>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void CompileDictionary(List<Action> list)
    {
        foreach(var Action in list)
        {
            if(!actionsDict.ContainsKey(Action.effect))
            {
                actionsDict.Add(Action.effect, new List<Action>() { Action });
            }
            else
            {
                actionsDict[Action.effect].Add(Action);
            }

            Action.ResetActions();
        }
    }

    public List<Action> CreatePlanner(DummyCharacter source)
    {
        CompileDictionary(source.actionList);

        List<Action> openList = new List<Action>();
        List<Action> closedList = new List<Action>();

        int currentEnergy = source.energy;
        foreach (var goal in goals)
        {
            openList.Add(goal);
            goal.energyLeft = currentEnergy;

            while (openList.Count > 0)
            {
                Action action = FindHighestPriority(openList);

                openList.Remove(action);
                closedList.Add(action);

                if (action.preCon == "")
                {
                    return ReturnPlan(action); // Change into return plan function
                }

                if (action.energyLeft <= 0)
                {
                    break;
                }

                foreach (var item in actionsDict[action.preCon])
                {
                    if (closedList.Contains(item)) { } //Do Nothing
                    else if (openList.Contains(item)) //Found new plan with lesser energy cost to reach goal
                    {
                        int tempEnergyLeft = action.energyLeft - item.energyUsed;

                        if (tempEnergyLeft > item.energyLeft)
                        {
                            item.parent = action;
                            item.energyLeft = tempEnergyLeft;
                        }
                    }
                    else //Continuing to explore current action plan
                    {
                        if (!action.CD)
                        {
                            item.parent = action;

                            item.energyLeft = action.energyLeft - item.energyUsed;
                            openList.Add(item);
                        }
                    }
                }
            }
        }

        return null;
    }

    Action FindHighestPriority(List<Action> list)
    {
        Action highest = list[0];
        foreach(var Action in list)
        {
            if (Action.priority > highest.priority)
                highest = Action;
        }

        return highest;
    }

    List<Action> ReturnPlan(Action action)
    {
        List<Action> plan = new List<Action>();

        Action temp= action;
        while(temp != null)
        {
            plan.Add(temp);
        }

        return plan;
    }
}