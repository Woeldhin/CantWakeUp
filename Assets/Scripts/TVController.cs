using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour
{
    public GameObject firstLaser;
    public Rigidbody key;

    public void TurnOn()
    {
        firstLaser.SendMessage("GoodJob");
        key.useGravity = true;
        key.tag = "Interactable";
    }
}
