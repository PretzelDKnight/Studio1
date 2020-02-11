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

    public bool isSupport = false;

    public bool move = false;

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

    public bool Move(GOAPAction nextAction)
    {
        this.transform.position +=(nextAction.target.transform.position - transform.position) * speed * Time.deltaTime;

        float distance = Vector3.Distance(nextAction.target.transform.position, transform.position);

        if (distance <= range)
        {
            nextAction.InRange = true;
            return true;
        }
        else
            return false;
    }

    public bool CheckMostFatal()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
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
            //target = dummy;
            return true;
        }
        else
            return false;
    }

    public bool CheckLowestHealth()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
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
            //target = dummy;
            return true;
        }
        else
            return false;
    }

    public bool CheckNearest()
    {
        DummyCharacter dummy = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
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
            //target = dummy;
            return true;
        }
        else
            return false;
    }

    public void ReturnActionPlan()
    {

    }
}
