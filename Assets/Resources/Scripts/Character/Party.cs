using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] List<Character> members;
    [SerializeField] bool AI;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveChildren();

        if (AI)
            Convert();
    }

    public List<Character> Members()
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
        members.Remove(chara);
    }

    public void AddMember(Character chara)
    {
        bool added = false;

        for (int i = 0; i < members.Count; i++)
        {
            if (members[i] == chara)
            {
                added = true;
            }
        }

        if (!added && members.Count == 3)
        {
            Debug.Log("Character Not Added!! Party may be full!");
        }
        else if (!added)
            members.Add(chara);
    }

    void RetrieveChildren()
    {
        var temp = GetComponentsInChildren<Character>();
        
        for (int i = 0; i < temp.Length; i++)
        {
            members.Add(temp[i]);
        }
    }

    public int ReturnCount()
    {
        return members.Count;
    }
}
