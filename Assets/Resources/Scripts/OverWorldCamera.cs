using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour, CameraInterface
{
    [SerializeField] GameObject player;

    Vector3 camOffset = new Vector3(-5.2f, 3.4f, -2f);

    public void Update()
    {
        transform.LookAt(player.transform);    
    }
}
