using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventListener : MonoBehaviour
{
    public MenuEvent menuEvent;

    public UnityEngine.Events.UnityEvent response;

    private void Start()
    {
        if(menuEvent!=null)
        {
            if(MenuEvent.CheckListener(this))
            {
                MenuEvent.AddListener(this);
            }
        }
    }

    public void OnCallEvent()
    {
        response.Invoke();
    }
}
