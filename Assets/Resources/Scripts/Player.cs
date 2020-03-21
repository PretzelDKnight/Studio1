using System.Collections.Generic;
using UnityEngine;

// SINGLETON CLASS!
public class Player : MonoBehaviour
{
    public static Player instance = null;

    public Character protagonist;
    public Party allies;

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

    // Function to load player from save
    void LoadPlayer()
    {

    }

    // Function to save player info to a file
    void SavePlayer()
    {

    }
}