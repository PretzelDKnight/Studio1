using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacter : MonoBehaviour
{
    public int health;
    public int armour;
    public int damage;
    public int energy;
    public int basicRange;
    public int s1Range;
    public int s2Range;
    public int tilesMoved;

    DummyCharacter target;

    public List<Action> actionList = new List<Action>();

    bool isSupport = false;

    bool SkillOneCD = false;
    bool SkillTwoCD = false;
    
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
        Debug.Log("Skill Two kameeeeeeeee   aaaaammmmeeeeeee   copyright strike!");
    }

    public void Move()
    {
        //this.transform.position = new Vector3(this.transform.position.x + 10 ,0,0);
        Debug.Log(tilesMoved + " tiles closer to " + target);
    }

    public bool CheckMostFatal()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, basicRange);
        int dmg = 0;
        foreach (var item in hitColliders)
        {
            if (item.tag != this.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                int dmgDeal = this.damage - temp.armour;
                if (dmgDeal > dmg)
                    dummy = temp;
            }
        }

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false;
    }

    public bool CheckLowestHealth()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, basicRange);
        int health = 100;
        foreach (var item in hitColliders)
        {
            if (item.tag != this.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                if (health > temp.health)
                    dummy = temp;
            }
        }

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false;
    }

    public bool CheckNearest()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, basicRange);
        int dist = 100;
        foreach (var item in hitColliders)
        {
            if (item.tag != this.tag)
            {
                DummyCharacter temp = item.GetComponent<DummyCharacter>();
                if (dist > Vector3.Distance(this.transform.position, temp.transform.position))
                    dummy = temp;
            }
        }

        if (dummy != null)
        {
            target = dummy;
            return true;
        }
        else
            return false;
    }

    int CheckDistToRange()
    {
        float dist = Vector3.Distance(this.transform.position, target.transform.position);
                       
        return (int)dist;
    }

    public void ReturnActionPlan()
    {
        foreach (var item in GOAP.instance.CreatePlanner(this))
        {

        }
    }
}
