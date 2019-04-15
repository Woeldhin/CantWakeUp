using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockWheel : MonoBehaviour
{
    public CombinationLock thisLock;
    public int steps;
    public int startStep;
    public int currentStep;

    private void Start()
    {
        startStep = (int)Random.Range(0f, 9.9999f);
        currentStep = startStep;
        transform.Rotate(0, 360/steps*startStep, 0);
    }

    private void Interact()
    {
        transform.Rotate(0, 360/steps, 0);
        currentStep++;
        if (currentStep == 10)
        {
            currentStep = 0;
        }
        thisLock.CheckIfCorrect();
    }
}
