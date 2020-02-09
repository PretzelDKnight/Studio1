using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public static Player instance = null;

    public Character proto;
    public Character proto1;
    public Character proto2;

    public CharacterListVariable allies;
    public PartyVariable party;

    // Save specific variables
    Vector3 position;
    GameObject map;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        allies.list.Add(proto);
        party.members[0] = proto;
        party.members[1] = proto1;
        party.members[2] = proto2;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Returns the list of Characters available to the Player
    /// </summary>
    /// <returns></returns>
    public List<Character> GetAllies()
    {
        return allies.list;
    }

    /// <summary>
    /// Adds a Character to the list of characters available to the Player
    /// </summary>
    /// <param name="chara"></param>
    public void AddAlly(Character chara)
    {
        allies.list.Add(chara);
    }

    /// <summary>
    /// Removes a Character from the list of those available to the Player (Character also deleted from the Party)
    /// </summary>
    /// <param name="chara"></param>
    public void DeleteAlly(Character chara)
    {
        allies.list.Remove(chara);

        for (int i = 0; i < party.members.Length; i++)
        {
            if (party.members[i] == chara)
            {
                party.members[i] = null;
            }
        }
    }

    /// <summary>
    /// Returns the array of Characters in the Players Party
    /// </summary>
    /// <returns></returns>
    public List<Character> GetParty()
    {
        List<Character> temp = new List<Character>();
        foreach (var item in party.members)
        {
            if (item != null)
            temp.Add(item);
        }
        return temp;
    }

    /// <summary>
    /// Adds a Party member to the party 
    /// </summary>
    /// <param name="chara"></param>
    public void AddPartyMember(Character chara)
    {
        bool added = false;
        for (int i = 0; i < party.members.Length; i++)
        {
            if (party.members[i] == null)
            {
                party.members[i] = chara;
                added = true;
            }
        }

        if (!added)
        {
            Debug.Log("Character Not Added!! Party may be full!");
        }
    }

    /// <summary>
    /// Switches a Character (sIn) into the party and takes Character (sOut) out
    /// </summary>
    /// <param name="chara"></param>
    public void SwitchPartyMember(Character sOut, Character sIn)
    {
        bool added = false;
        for (int i = 0; i < party.members.Length; i++)
        {
            if (party.members[i] == sOut)
            {
                party.members[i] = sIn;
                added = true;
            }
        }

        if (!added)
        {
            Debug.Log("Character Not Added!! Character may not Exist!");
        }
    }

    /// <summary>
    /// Deletes a given Character from the Party (Character still exists in Allies)
    /// </summary>
    /// <param name="chara"></param>
    public void DeletePartyMember(Character chara)
    {
        for (int i = 0; i < party.members.Length; i++)
        {
            if (party.members[i] == chara)
            {
                party.members[i] = null;
            }
        }
    }


    void LoadPlayer()
    {

    }

    void SavePlayer()
    {

    }
}
