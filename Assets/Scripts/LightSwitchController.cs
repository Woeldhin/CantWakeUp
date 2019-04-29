using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    //Boolean to check are lights on or off
    public bool isLightsOn;
    
    //Gameobjects for light and switch
    public GameObject SpotLight;
    public GameObject Switch;

    // Check at start is light on or off and do stuff accordingly
    void Start()
    {
        if(SpotLight.activeSelf == true)
        {
            isLightsOn = true;
            Switch.transform.localEulerAngles = new Vector3(10, 0, 0);
        }
        else
        {
            isLightsOn = false;
            Switch.transform.localEulerAngles = new Vector3(-10, 0, 0);
        }
    }

    //Interacting with lightSwitch ...
    void Interact()
    {
        Debug.Log("LightSwitch!");
        if(isLightsOn == true)
        {
            isLightsOn = false;
            SpotLight.SetActive(false);
            Switch.transform.localEulerAngles = new Vector3(-10, 0, 0);
        }
        else
        {
            isLightsOn = true;
            SpotLight.SetActive(true);
            Switch.transform.localEulerAngles = new Vector3(10, 0, 0);
        }
    }
}
