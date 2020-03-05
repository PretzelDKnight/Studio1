using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpin : MonoBehaviour
{
    public Vector3 rotation;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRot = transform.rotation.eulerAngles;
        currentRot += rotation * Time.deltaTime * speed;

        transform.rotation = Quaternion.Euler(currentRot);
    }
}
