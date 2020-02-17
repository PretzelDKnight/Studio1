using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCamera : MonoBehaviour, CameraInterface
{
    [SerializeField] GameObject player;
    
    [SerializeField] List<CameraPlacements> placement;

    [HideInInspector] public Vector3 camOffset = new Vector3(-5.2f, 3.4f, -2f);

    public void Update()
    {
        Camera.main.transform.position = player.transform.position + camOffset;        
    }

    public void ShiftCameraAngle(string battle)
    {
        Vector3 temp;
        if(battle == "CameraPoint1")
        {
            temp = placement[0].cameraRot;
            camOffset.z = placement[0].offsetZ;
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(Camera.main.transform.rotation.x, temp.y, Camera.main.transform.rotation.z)); 
        }
        else if(battle == "CameraPoint2")
        {
            temp = placement[1].cameraRot;
            camOffset.z = placement[1].offsetZ;
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(Camera.main.transform.rotation.x, temp.y, Camera.main.transform.rotation.z));
        }
        else if(battle == "CameraPoint3")
        {            
            temp = placement[2].cameraRot;
            camOffset.z = placement[2].offsetZ;
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(Camera.main.transform.rotation.x, temp.y, Camera.main.transform.rotation.z));
        }
    }
}
