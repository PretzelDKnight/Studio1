using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour
{
    [SerializeField] GameObject player;

    public Vector3 camOffset;

    public float scrollSpeed = 2f;
    public float minHeight = 20f;
    public float maxHeight = 120f;

    public Vector2 limit;

    //Ray ray;

    private void Update()
    {       
        Vector3 pos = transform.position;

        this.transform.position = player.transform.position + camOffset;

        transform.LookAt(player.transform);

        //Physics.RaycastAll(this.transform.position, player.transform.position, 1000, 9); #Will add function to fade objects in the way later

        float scrollVar = Input.GetAxis("Mouse ScrollWheel");

        pos.y = scrollVar * scrollSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);
        pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
        pos.z = Mathf.Clamp(pos.z, -limit.y, limit.y);
    }
}
