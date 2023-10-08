using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public RenderTexture rt;
    public Camera camera;
    public Material material;

    /// <summary>
    ///Creates a RenderTexture at the start of the execution and associates it with the camera and the material of the plane
    /// </summary>
    void Start()
    {
        rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        camera.targetTexture = rt;
        material.mainTexture = rt;
    }

}
