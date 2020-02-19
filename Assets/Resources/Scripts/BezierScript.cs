using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierScript : MonoBehaviour
{
    public BezierPlayer player;
    
    public Transform p0, p1, p2 ,p3;

    [Range(0,1)]
    public float time;

    private void Update()
    {
        //Bezier formula for quadratic curve = (1−t)^2.P0 + 2t.(1−t).P1 + t^2.P2

        time = player.distance;

        transform.position = Mathf.Pow((1 - time), 3) * p0.position + 3 * time * Mathf.Pow((1 - time), 2) * p1.position + 3 * time * time *(1 - time) * p2.position + Mathf.Pow(time, 3) * p3.position;
    }
}
