using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    public Vector3 camOffset;

    private void Update()
    {       
        this.transform.position = player.transform.position + camOffset;

        transform.LookAt(player.transform);        
    }
}
