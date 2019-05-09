using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject nextSwitch;
    public TVController tv;
    public bool lastSwitch;
    public string changeText;

    void Interact()
    {
        if(lastSwitch)
        {
            tv.Next();
        }
        else
        {
            nextSwitch.SetActive(true);
            tv.mirrorText.text = changeText;
        }
    }
}
