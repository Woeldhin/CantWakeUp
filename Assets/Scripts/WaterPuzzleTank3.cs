using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuzzleTank3 : MonoBehaviour
{
    GameObject tap;
    GameObject tank5;

    GameObject tank3bar1;
    GameObject tank3bar2;
    GameObject tank3bar3;

    GameObject tank5bar1;
    GameObject tank5bar2;
    GameObject tank5bar3;
    GameObject tank5bar4;
    GameObject tank5bar5;

    public int script5count;
    public int realcount;
    public int rest;

    public int count3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        tap = GameObject.Find("Tap");
        tank5 = GameObject.Find("Tank5");

        tank3bar1 = GameObject.Find("Tank3Bar1");
        tank3bar2 = GameObject.Find("Tank3Bar2");
        tank3bar3 = GameObject.Find("Tank3Bar3");

        tank5bar1 = GameObject.Find("Tank5Bar1");
        tank5bar2 = GameObject.Find("Tank5Bar2");
        tank5bar3 = GameObject.Find("Tank5Bar3");
        tank5bar4 = GameObject.Find("Tank5Bar4");
        tank5bar5 = GameObject.Find("Tank5Bar5");

        script5count = GameObject.Find("Tank5").GetComponent<WaterPuzzleTank5>().count5;
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

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tap.GetComponent<Renderer>().material.color == Color.red))
        {
            count3 = 3;
            tank3bar1.GetComponent<Renderer>().material.color = Color.cyan;
            tank3bar2.GetComponent<Renderer>().material.color = Color.cyan;
            tank3bar3.GetComponent<Renderer>().material.color = Color.cyan;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tap.GetComponent<Renderer>().material.color = Color.white;

        }

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tank5.GetComponent<Renderer>().material.color == Color.red))
        {
            realcount = count3 + GameObject.Find("Tank5").GetComponent<WaterPuzzleTank5>().count5;

            if (realcount > 3)
            {
                rest = realcount - 3;
                realcount -= rest;
            }

            switch (realcount)
            {
                case 3:
                    tank3bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank3bar3.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 2:
                    tank3bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank3bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 1:
                    tank3bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 0:
                    tank3bar1.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar3.GetComponent<Renderer>().material.color = Color.white;
                    break;
                default:
                    print("Error");
                    break;
            }

            count3 = realcount;

            GameObject.Find("Tank5").GetComponent<WaterPuzzleTank5>().count5 = rest;

            switch (rest)
            {

                case 5:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 4:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    print("You win!");
                    break;
                case 3:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 2:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.cyan;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 1:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case 0:
                    tank5bar1.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    break;
                default:
                    print("Error");
                    break;
            }

            rest = 0;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tank5.GetComponent<Renderer>().material.color = Color.white;
        }
    }


}
