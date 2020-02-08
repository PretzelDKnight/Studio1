using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {

    }

    public bool Interactable
    {
        get { return button.interactable; }
        set { button.interactable = value; }
    }
}
