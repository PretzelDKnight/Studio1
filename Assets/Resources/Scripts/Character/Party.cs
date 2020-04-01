using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] Character[] members;
    [SerializeField] bool AI;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveChildren();

        if (AI)
            Convert();
    }

    public Character[] Members()
    {
        return members;
    }

    void Convert()
    {
        foreach (var member in members)
        {
            member.AI = true;
        }
    }

    public void DeleteMember(Character chara)
    {
        for (int i = 0; i < members.Length; i++)
        {
            if (members[i] == chara)
            {
                members[i] = null;
            }
        }
    }

    public void AddMember(Character chara)
    {
        bool added = false;
        for (int i = 0; i < members.Length; i++)
        {
            if (members[i] == null)
            {
                members[i] = chara;
                added = true;
            }
        }

        if (!added)
        {
            Debug.Log("Character Not Added!! Party may be full!");
        }
    }

    void RetrieveChildren()
    {
        members = GetComponentsInChildren<Character>();
    }

    public int ReturnCount()
    {
        return members.Length;
    }
}
