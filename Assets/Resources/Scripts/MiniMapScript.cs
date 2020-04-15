using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public Transform player;

    Vector3 newPos;

    private void LateUpdate()
    {
        newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f, player.eulerAngles.y, 0), 10f);
    }
}
