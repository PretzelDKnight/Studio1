using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject nextChoice;
    public GameObject currentChoice;

    public void Next()
    {
        nextChoice.gameObject.SetActive(true);
        currentChoice.gameObject.SetActive(false);
    }
}
