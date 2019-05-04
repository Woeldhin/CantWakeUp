﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float[] angles = { 0, 95 };
    public int openState;
    public GameObject parent;
    private float currentAngle;
    public bool locked;

    //Audio stuff//
    //Audio clips
    public AudioClip openDoor;
    public AudioClip closeDoor;
    //Audio sources
    public AudioSource forOpeningDoor;
    public AudioSource forClosingDoor;

    // Start is called before the first frame update
    void Start()
    {
        openState = 0;
        //Setting audioclips to audio sources
        forOpeningDoor.clip = openDoor;
        forClosingDoor.clip = closeDoor;
    }

    void Update()
    {
        if (parent.transform.localEulerAngles.y != angles[openState])
        {
            currentAngle = Mathf.Lerp(parent.transform.localEulerAngles.y, angles[openState], 0.05f);

            parent.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
        }
    }

    void OpenAndClose()
    {
        if (openState == 0)
        {
            openState = 1;
            //Play audio for opening door
            forOpeningDoor.Play();
        }
        else
        {
            openState = 0;
            //Play audio for closing door
            forClosingDoor.PlayDelayed(0.3f);
        }
    }

    void Interact()
    {
        if (locked)
        {
            Debug.Log("It's locked.");
        }
        else
        {
            OpenAndClose();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Key"))
        {
            locked = !locked;
            collision.transform.gameObject.SetActive(false);
        }
    }
}
