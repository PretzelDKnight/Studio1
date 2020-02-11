using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;
    
    [SerializeField] List<GOAPAction> goals;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public List<GOAPAction> plan(DummyCharacter character, HashSet<GOAPAction> availableActions)
    {
        // Reset Actions
        foreach (GOAPAction a in availableActions)
        {
            a.ResetAction();
        }
        
        // Check the usable actions based on their PreCons
        HashSet<GOAPAction> usableActions = new HashSet<GOAPAction>();
        foreach (GOAPAction a in availableActions)
        {
            if (a.CheckPrecon(character))
                usableActions.Add(a);
        }

        List<GOAPAction> resultList = new List<GOAPAction>();
        while()
        {

        }

        return null;
    }
}