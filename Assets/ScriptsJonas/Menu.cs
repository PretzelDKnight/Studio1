using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transform[] VirtualScenes;
    public float Speed;
    public Transform CurrentScene;
    public GameObject MainMenu;


// Start is called before the first frame update
void Start()
    {
        CurrentScene = VirtualScenes[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CurrentScene.position, Time.deltaTime * Speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, CurrentScene.rotation, Time.deltaTime * Speed);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CurrentScene = VirtualScenes[1];
            MainMenu.SetActive(false);
        }
    }
}
