using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCam : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float borderThickness = 10f;
    public Vector2 limit;
    public float scrollSpeed = 2f;
    public float minHeight = 20f;
    public float maxHeight = 120f;

    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - borderThickness)
        {
            pos.z += moveSpeed + Time.deltaTime;
        }
        if (Input.mousePosition.y <= borderThickness)
        {
            pos.z -= moveSpeed + Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - borderThickness)
        {
            pos.x += moveSpeed + Time.deltaTime;
        }
        if (Input.mousePosition.x <= borderThickness)
        {
            pos.x -= moveSpeed + Time.deltaTime;
        }

        float scrollVar = Input.GetAxis("Mouse ScrollWheel");

        pos.y = scrollVar * scrollSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);
        pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
        pos.z = Mathf.Clamp(pos.z, -limit.y, limit.y);

        transform.position = pos;
    }
}
