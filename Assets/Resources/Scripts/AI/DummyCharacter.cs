using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    public int health;
    public int armour;
    public int damage;
    public int energy;
    public int range;
    public int tilesMoved;
    public int speed;

    DummyCharacter target;

    public bool isSupport = false;

    public bool move = false;

    public bool SkillOneCD = false;
    public bool SkillTwoCD = false;

    HashSet<KeyValuePair<string, object>> AIGoals = new HashSet<KeyValuePair<string, object>>();

    public HashSet<GOAPAction> actionList = new HashSet<GOAPAction>();
    private void Start()
    {
        AddGoal();
    }

    void AddGoal()
    {
        AIGoals.Add(new KeyValuePair<string, object>("kAttack", false));
    }

    public void BasicAttack(DummyCharacter target)
    {
        Debug.Log("Basic Attack take that");
    }

    // Function for Vangaurd Skill 1
    public void SkillOne(DummyCharacter target)
    {
        Debug.Log("Skill One hayaaaahhh");
    }

    // Function for Vangaurd Skill 2
    public void SkillTwo(DummyCharacter target)
    {
        Debug.Log("Skill Two kameeeeeeeee   haaaaammmmeeeeeee   copyright strike!");
    }

    public void Move()
    {
        
    }

    public void ReturnActionPlan()
    {
        Queue<GOAPAction> planQueue = GOAP.instance.plan(this, actionList, AIGoals);

        foreach (var item in planQueue)
        {
            item.Execute(this);
        }
    }
}
