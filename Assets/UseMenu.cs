using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMenu : MonoBehaviour
{
    CharacterController player;

    [SerializeField] GameObject eButton;

    bool isMenu;

    bool returnbool;

    Vector3 barCamPos;
    Quaternion barCamRot;

    [SerializeField] Vector3 menuCamPos;
    [SerializeField] Vector3 menuCamRotVector;

    Quaternion menuCamRot = new Quaternion();    

    private void Start()
    {
        barCamPos = Camera.main.transform.position;
        barCamRot = Camera.main.transform.rotation;        

        player = FindObjectOfType<CharacterController>();

        menuCamRot = Quaternion.Euler(menuCamRotVector);

        Camera.main.transform.position = menuCamPos;
        Camera.main.transform.rotation = menuCamRot;

        eButton.SetActive(false);

        returnbool = false;
        player.able = false;
    }

    private void Update()
    {
        Debug.Log(returnbool);

        if (isMenu && returnbool)
            Menufunc();
        else if (isMenu)
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
            eButton.SetActive(false);

            //Change camera position to menu
            Camera.main.transform.position = menuCamPos;
            Camera.main.transform.rotation = menuCamRot;
            //Check for menu functions and stuff
            player.able = false;
            returnbool = false;
        }
    }

    public void SetRet()
    {
        returnbool = true;
        player.able = true;

        Camera.main.transform.position = barCamPos;
        Camera.main.transform.rotation = barCamRot;
    }
}
