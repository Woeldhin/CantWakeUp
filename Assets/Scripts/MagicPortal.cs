using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPortal : MonoBehaviour
{

    [SerializeField]
    CharacterMovement player;
    [SerializeField]
    MagicMirror magicMirror;

    // delegate for calling all the units that must turn when mirror is used
    public delegate void CallMirrorVisibilitys(bool exist);
    public CallMirrorVisibilitys callMirrorVisibilitys;

    public bool active = true;
    public bool flip = true;
    bool oldFlip;

    //-----The Interfaces-----

    /// Set true to allow player to use the portal
    public void Activate()
    {
        active = !active;
    }
    
    public void Activate(bool isActive)
    {
        active = isActive;
    }
    
    /// Set true to flip the mirrors image verticaly
    public void FlipMirror()
    {
        flip = !flip;
        SetMirror();
    }
    
    public void FlipMirror(bool flipped)
    {
        flip = flipped;
        SetMirror();
    }
    //-----End Of Interfaces-----


    void Awake()
    {
        oldFlip = flip;
    }

    // startup init
    void Start()
    {
        player.Flipped(false);
        player.Mirroring(false);

        magicMirror.Flipped(false);
        magicMirror.Mirroring(true);
    }



    // for detecting the change in flip from the inspector
    /// !!!disable for final build!!!
    void Update()
    {
        if(oldFlip != flip)
        {
            oldFlip = flip;
            SetMirror();
        }
    }

    // manages the teleportation of the player
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && active)
        {
            player.Flip(this.gameObject, flip);

            if (flip)
            {
                player.Flipped(!player.flipped);
            } else
            {
                player.Mirroring(!player.mirrorCamera);
            }
            SetMirror();
            
            // set objects to exist if players camera is mirrored
            callMirrorVisibilitys(player.mirrorCamera);

        }
    }

    // Sets the mirror bools right
    private void SetMirror()
    {
        //Debug.Log("SetMirror:" + flip);
        if (flip)
        {
            magicMirror.Flipped(!player.flipped);
            magicMirror.Mirroring(player.mirrorCamera);
            magicMirror.playerUpright = !player.flipped;
        }
        else
        {
            magicMirror.Flipped(player.flipped);
            magicMirror.Mirroring(!player.mirrorCamera);
        }
    }

}
