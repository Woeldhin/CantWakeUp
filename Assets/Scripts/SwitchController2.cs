using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController2 : MonoBehaviour
{

    public bool isButtonPressed;

    public GameObject Switch2;
    public GameObject Switch3;
    // Start is called before the first frame update
    void Start()
    {
        Switch3.SetActive(false);
    }

    // Update is called once per frame
    void Interact()
    {

        Debug.Log("Button active!");
        Switch3.SetActive(true);
        
    }
}



