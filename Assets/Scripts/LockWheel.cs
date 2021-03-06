﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockWheel : MonoBehaviour
{
    public CombinationLock thisLock;
    public Text visionThing; // Hopefully temporary
    public int steps;
    public int startStep;
    public int currentStep;
    //Audio stuff//
    //Clips
    public AudioClip numericLock;
    //Sources
    public AudioSource sourceForLock;

    private void Start()
    {
        startStep = (int)Random.Range(0f, 9.9999f);
        currentStep = startStep;
        transform.Rotate(0, 360/steps*startStep, 0);
        visionThing.text = currentStep.ToString();
        //Putting sounds to sources
        sourceForLock.clip = numericLock;
    }

    private void Interact()
    {
        sourceForLock.Play();
        transform.Rotate(0, 360/steps, 0);
        currentStep++;
        if (currentStep == 10)
        {
            currentStep = 0;
        }
        thisLock.CheckIfCorrect();
        visionThing.text = currentStep.ToString();
    }
}
