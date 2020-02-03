using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public static Player instance = null;

    List<Character> allies;
    Character[] party = new Character[3];

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
        return allies;
    }

    /// <summary>
    /// Adds a Character to the list of characters available to the Player
    /// </summary>
    /// <param name="chara"></param>
    public void AddAlly(Character chara)
    {
        allies.Add(chara);
    }

    /// <summary>
    /// Removes a Character from the list of those available to the Player (Character also deleted from the Party)
    /// </summary>
    /// <param name="chara"></param>
    public void DeleteAlly(Character chara)
    {
        allies.Remove(chara);

        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == chara)
            {
                party[i] = null;
            }
        }
    }

    /// <summary>
    /// Returns the array of Characters in the Players Party
    /// </summary>
    /// <returns></returns>
    public Character[] GetParty()
    {
        return party;
    }

    /// <summary>
    /// Adds a Party member to the party 
    /// </summary>
    /// <param name="chara"></param>
    public void AddPartyMember(Character chara)
    {
        bool added = false;
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == null)
            {
                party[i] = chara;
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
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == sOut)
            {
                party[i] = sIn;
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
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == chara)
            {
                party[i] = null;
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
