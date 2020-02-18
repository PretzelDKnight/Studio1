using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMenu : MonoBehaviour
{
    CharacterController player;

    [SerializeField] GameObject eButton;

    bool isMenu;

    bool returnbool;

    [SerializeField] Vector3 menuCamPos;
    [SerializeField] Vector3 menuCamRotVector;

    Quaternion menuCamRot = new Quaternion();    

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
        menuCamRot = Quaternion.Euler(menuCamRotVector);
        eButton.SetActive(false);
    }

    private void Update()
    {
        if (isMenu)
            Menufunc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            eButton.SetActive(true);
            isMenu = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            eButton.SetActive(false);
    }

    void Menufunc()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Change camera position to menu
            Camera.main.transform.position = menuCamPos;
            Camera.main.transform.rotation = menuCamRot;
            //Check for menu functions and stuff
            player.able = false;
            returnbool = false;
        }
        else
            player.able = true;
    }

    public void SetRet()
    {
        returnbool = true;
    }
}
