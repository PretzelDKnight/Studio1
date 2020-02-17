using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct CameraPlacements
{
    [SerializeField] public Vector3 cameraRot;
    [SerializeField] public float offsetZ;
}

interface CameraInterface
{
    void Update();
}
