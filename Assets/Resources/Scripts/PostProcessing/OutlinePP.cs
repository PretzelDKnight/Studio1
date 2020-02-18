using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class OutlinePP : MonoBehaviour
{
    public Material outline;

    private Camera cam;

    private void Start()
    {
        //get the camera and tell it to render a depthnormals texture
        cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.DepthNormals;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, outline, 0);
    }
}
