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

    public bool SkillOneCD = false;
    public bool SkillTwoCD = false;

    public List<GOAPAction> actionList = new List<GOAPAction>();

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

    }
}
