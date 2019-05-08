using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationLock : MonoBehaviour
{
    public GameObject lockedObject;
    public LockWheel n1;
    public LockWheel n2;
    public LockWheel n3;
    public Text n1Text;
    public Text n2Text;
    public Text n3Text;
    private int n1Correct;
    private int n2Correct;
    private int n3Correct;
    
    
    private void Start()
    {
        n1Correct = (int)Random.Range(0f, 9.9999f);
        n2Correct = (int)Random.Range(0f, 9.9999f);
        n3Correct = (int)Random.Range(0f, 9.9999f);
        n1Text.text = n1Correct.ToString();
        n2Text.text = n2Correct.ToString();
        n3Text.text = n3Correct.ToString();
        
    }
    public void CheckIfCorrect()
    {
        if (n1.currentStep == n1Correct && n2.currentStep == n2Correct && n3.currentStep == n3Correct)
        {
            StartCoroutine(Unlock());
        }
    }
    IEnumerator Unlock()
    {
        // I guess something happens 
        Debug.Log("Mellon");
        // Open the locked Object
        while (lockedObject.transform.position.y < 6.5f)
        {
            lockedObject.transform.position = new Vector3(lockedObject.transform.position.x, Mathf.Lerp(lockedObject.transform.position.y, 6.5f, 0.001f), lockedObject.transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
