using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour
{
    public GameObject firstLaser;
    public Rigidbody key;
    public GameObject tvViewReal;
    public GameObject tvViewMirror;
    public MagicPortal portal;

    public void TurnOn()
    {
        firstLaser.SendMessage("GoodJob");
        //key.useGravity = true;
        //key.tag = "Interactable";

        tvViewReal.SetActive(true);
        tvViewMirror.SetActive(true);
        portal.Activate(true);
    }
}
