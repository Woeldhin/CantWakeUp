using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraMirror : MonoBehaviour
{
    public bool mirror;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void OnPreCull()
    {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        Vector3 scale = new Vector3(mirror ? -1 : 1, 1, 1);
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(scale);
    }
    void OnPreRender()
    {
        GL.invertCulling = mirror;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }

}
