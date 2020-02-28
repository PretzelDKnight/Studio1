using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPlayer : MonoBehaviour
{
    public Transform start, end;

    [Range(0,1)] public float distance;

    void Update()
    {
        float magnitude = Vector3.Distance(start.position, end.position);
        float playerDist = Vector3.Distance(start.position, transform.position);

        distance = playerDist / magnitude;
    }
}
