using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuzzleTap : MonoBehaviour
{
    GameObject tank5;
    GameObject tank5bar1;
    GameObject tank5bar2;
    GameObject tank5bar3;
    GameObject tank5bar4;
    GameObject tank5bar5;

    GameObject tank3;
    GameObject tank3bar1;
    GameObject tank3bar2;
    GameObject tank3bar3;

    // Start is called before the first frame update
    void Start()
    {
        tank5 = GameObject.Find("Tank5");
        tank5bar1 = GameObject.Find("Tank5Bar1");
        tank5bar2 = GameObject.Find("Tank5Bar2");
        tank5bar3 = GameObject.Find("Tank5Bar3");
        tank5bar4 = GameObject.Find("Tank5Bar4");
        tank5bar5 = GameObject.Find("Tank5Bar5");

        tank3 = GameObject.Find("Tank3");
        tank3bar1 = GameObject.Find("Tank3Bar1");
        tank3bar2 = GameObject.Find("Tank3Bar2");
        tank3bar3 = GameObject.Find("Tank3Bar3");

    }

    void Interact()
    {
        if (gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tank5.GetComponent<Renderer>().material.color == Color.red))
        {
            tank5bar1.GetComponent<Renderer>().material.color = Color.white;
            tank5bar2.GetComponent<Renderer>().material.color = Color.white;
            tank5bar3.GetComponent<Renderer>().material.color = Color.white;
            tank5bar4.GetComponent<Renderer>().material.color = Color.white;
            tank5bar5.GetComponent<Renderer>().material.color = Color.white;

            GameObject.Find("Tank5").GetComponent<WaterPuzzleTank5>().count5 = 0;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tank5.GetComponent<Renderer>().material.color = Color.white;
        }

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tank3.GetComponent<Renderer>().material.color == Color.red))
        {
            tank3bar1.GetComponent<Renderer>().material.color = Color.white;
            tank3bar2.GetComponent<Renderer>().material.color = Color.white;
            tank3bar3.GetComponent<Renderer>().material.color = Color.white;

            GameObject.Find("Tank3").GetComponent<WaterPuzzleTank3>().count3 = 0;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tank3.GetComponent<Renderer>().material.color = Color.white;
        }

    }
}
