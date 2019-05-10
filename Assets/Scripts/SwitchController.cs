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
        tv.mirrorText.text = changeText;

        if (lastSwitch)
        {
            tv.Next();
        }
        else
        {
            nextSwitch.SetActive(true);
        }
    }
}
