using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierScript : MonoBehaviour
{
    public BezierPlayer player;
    
    public Transform p0, p1, p2, p3;

    Vector3 a, b, c, d;

    [Range(0,1)]
    public float time;

    private void Update()
    {
        //Bezier formula for quadratic curve = (1−t)^2.P0 + 2t.(1−t).P1 + t^2.P2

        //Bezier formula for cubic curve = (1−t)^3.P0 + 3t.(1−t)^2.P1 + 3.(1-t).t^2.P2 + t^3.P3
        
        //Bezier formula for quartic curve = (1−t)^5.P0 + 5.t.(1-t)^4.P1 + 10.t^2.(1-t)^3.P2 + 10t^3.(1-t)^2.P3 +

        time = player.distance;

        a = Mathf.Pow((1 - time), 3) * p0.position;

        b = 3 * time * Mathf.Pow((1 - time), 2) * p1.position;

        c = 3 * (1 - time) * Mathf.Pow(time, 2) * p2.position;

        d = Mathf.Pow(time, 3) * p3.position;

        transform.position = a + b + c + d;
    }
}
