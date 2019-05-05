using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public bool isButtonPressed;

    public GameObject Switch1;
    public GameObject Switch2;
    public GameObject Switch3;




    // Start is called before the first frame update
    void Start()
    {
        Switch2.SetActive(false);
        Switch3.SetActive(false);
    }

   

    // Update is called once per frame
   void Update()
    {
      

        if (isButtonPressed == true)
        {
            Switch2.SetActive(true);
        }
       
        
        if (isButtonPressed == true)
        {
            Switch3.SetActive(true);
        }
       

    }

    void Interact()
    {
            isButtonPressed = true;


            Debug.Log("Button active!");

        
    }
}
