using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectsController : MonoBehaviour
{
    //6 digit codeblock on the wall that comes visible when lights switched off
    public GameObject CodeBlock;
    //Light that is monitored when switched off and on
    public GameObject SpotLight;
    
    // Making block that shows "code" not visible at start
    void Start()
    {
        CodeBlock.SetActive(false);
    }

    // check if lights are on or off and make block of code active or deactive accordingly
    void Update()
    {
        if (SpotLight.activeInHierarchy == false)
        {
            CodeBlock.SetActive(true);
        }
        else
        {
            CodeBlock.SetActive(false);
        }
    }
}
