using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CameraPlacements 
{
    Vector3 cameraPos;
    Quaternion cameraRot;
}

public class CameraScript : MonoBehaviour
{
    List<CameraPlacements> cameraPlacements = new List<CameraPlacements>();

    bool isBattle;
    // Start is called before the first frame update
    void Start()
    {
        isBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBattle)
        {
            //Camera functionality within the battle sequence

        }
        else
        {
            //Camera functionality within the overworld

        }
    }
}
