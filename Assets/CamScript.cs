using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    CharacterController player;

    [SerializeField] Vector3 camOffset;
    void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3((player.transform.position.x-20), player.transform.position.y, (player.transform.position.z - 19));
        this.transform.position = temp + camOffset;
    }
}
