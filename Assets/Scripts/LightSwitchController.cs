using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{

    public bool isLightsOn;



    public GameObject SpotLight;
    public GameObject Switch;

    // Start is called before the first frame update
    void Start()
    {
        if(SpotLight.activeInHierarchy == true)
        {
            isLightsOn = true;
            Switch.transform.eulerAngles = new Vector3(10, 0, 0);
        }
        else
        {
            isLightsOn = false;
            Switch.transform.eulerAngles = new Vector3(-10, 0, 0);
        }
    }

    void Interact()
    {
        Debug.Log("LightSwitch!");
        if(isLightsOn == true)
        {
            isLightsOn = false;
        }
        else
        {
            isLightsOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLightsOn == true)
        {
            SpotLight.SetActive(true);
        }
        else
        {
            SpotLight.SetActive(false);
        }
    }
}
