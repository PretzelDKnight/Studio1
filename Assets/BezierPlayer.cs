using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPlayer : MonoBehaviour
{
    public Transform start, end;

    [Range(0,1)] public float distance;

    private void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(start.position, end.position, distance);

        float magnitude = Vector3.Distance(start.position, end.position);
        float playerDist = Vector3.Distance(start.position, transform.position);

        distance = playerDist / magnitude;
    }
}
