using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CameraMirror))]
public class MagicMirror : MonoBehaviour
{

    [SerializeField]
    Transform playerCameraT, mirrorT;
    [SerializeField]
    Material cameraMaterial;
    [SerializeField]
    float roomHight = 3f;
    [SerializeField]
    public bool flipped = false;
    bool mirror = true;
    public bool playerUpright = true;

    Camera cam;
    CameraMirror cameraMirror;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cameraMirror = GetComponent<CameraMirror>();
    }

    // Set cameraTexture right for the screen size
    void Start()
    {
        // first delete previous texture if any
        if (cam.targetTexture != null)
        {
            cam.targetTexture.Release();
        }

        // make new texture with the screens resolutions
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // and pair it to the cameraMaterial
        cameraMaterial.mainTexture = cam.targetTexture;
    }

    // Set portalCameras position and rotation right
    void LateUpdate()
    {
        // ----position----

        // get players offset from portal
        Vector3 playerOffsetFromPortal = playerCameraT.position - mirrorT.position;
        // reflect the offset from the portals normal
        playerOffsetFromPortal = Vector3.Reflect(playerOffsetFromPortal, mirrorT.up);
        // add reflect and offset to get true target position
        Vector3 newPosition = mirrorT.position + playerOffsetFromPortal;

        if (flipped == playerUpright)
        {
            // update transform with roomhight correction
            transform.position = new Vector3(newPosition.x, roomHight - playerCameraT.position.y, newPosition.z);
        }
        else
        {
            // update transform
            transform.position = newPosition;
        }


        // ----rotation----

        Quaternion lookAtRotation;

        if (flipped == playerUpright)
        {
            // get playerCameras rotation and 'flip its x-component'
            Vector3 distortedPlayerCamera = Quaternion.AngleAxis(2 * (playerCameraT.eulerAngles.x + mirrorT.eulerAngles.x), (playerUpright) ? -playerCameraT.right : playerCameraT.right) * playerCameraT.forward;
            // reflect the distorted camera vector from the portals normal
            Vector3 newCameraRotation = Vector3.Reflect(distortedPlayerCamera, mirrorT.up);
            // convert to Quaternion and flip upsidedown
            lookAtRotation = Quaternion.LookRotation(newCameraRotation, (playerUpright) ? Vector3.down : Vector3.up);
        }
        else
        {
            // reflect camera vector from the portals normal
            Vector3 newCameraRotation = Vector3.Reflect(playerCameraT.forward, mirrorT.up);
            // convert to Quaternion and flip upsidedown
            lookAtRotation = Quaternion.LookRotation(newCameraRotation, (playerUpright) ? Vector3.up : Vector3.down);
        }


        // apply new rotation to transform
        transform.rotation = lookAtRotation;
    }

    // update mirroring bool
    public void Mirroring(bool mirrored)
    {
        mirror = mirrored;
        cameraMirror.mirror = mirror;
    }

    // update flipping bool
    public void Flipped(bool flipped)
    {
        this.flipped = flipped;
    }
}
