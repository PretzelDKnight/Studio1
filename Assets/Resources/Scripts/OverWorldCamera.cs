using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour, CameraInterface
{
    [SerializeField] GameObject player;

    public void Update()
    {
        transform.LookAt(player.transform);    
    }
}
