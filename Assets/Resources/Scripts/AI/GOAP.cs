using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GOAP : MonoBehaviour
{
    public static GOAP instance = null;
    
    [SerializeField] List<GOAPAction> goals;
    
    Dictionary<string, List<GOAPAction>> actionsDict = new Dictionary<string, List<GOAPAction>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void CompileDictionary(List<GOAPAction> list)
    {

    }

}