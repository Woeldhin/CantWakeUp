using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLock : MonoBehaviour
{
    public GameObject lockedObject;
    public LockWheel n1;
    public LockWheel n2;
    public LockWheel n3;
    public int n1Correct;
    public int n2Correct;
    public int n3Correct;

    private void Start()
    {
        n1Correct = (int)Random.Range(0f, 9.9999f);
        n2Correct = (int)Random.Range(0f, 9.9999f);
        n3Correct = (int)Random.Range(0f, 9.9999f);
    }
    public void CheckIfCorrect()
    {
        if (n1.currentStep == n1Correct && n2.currentStep == n2Correct && n3.currentStep == n3Correct)
        {
            Unlock();
        }
    }
    void Unlock()
    {
        // I guess something happens 
        Debug.Log("Mellon");
    }
}
