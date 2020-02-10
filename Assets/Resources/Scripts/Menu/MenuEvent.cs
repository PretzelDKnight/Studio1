using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    private static List<MenuEventListener> menuEventList;

    void Awake()
    {
        menuEventList = new List<MenuEventListener>();
    }

    public void Call()
    {
        foreach(MenuEventListener listener in menuEventList)
        {
            listener.OnCallEvent();
            Debug.Log("CALLING!!!");
        }
    }

    static public void AddListener(MenuEventListener listener)
    {
        menuEventList.Add(listener);
        Debug.Log("Added Listener");
    }

    static public bool CheckListener(MenuEventListener listener)
    {
        foreach (var item in menuEventList)
        {
            if (item == listener)
            {
                Debug.Log("Listener is not there");
                return false;
            }
        }
        Debug.Log("Listener is there");
        return true;
    }
}
