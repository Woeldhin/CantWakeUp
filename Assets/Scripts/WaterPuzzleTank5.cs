using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuzzleTank5 : MonoBehaviour

{
    GameObject tap;
    GameObject tank3;

    GameObject tank5bar1;
    GameObject tank5bar2;
    GameObject tank5bar3;
    GameObject tank5bar4;
    GameObject tank5bar5;

    GameObject tank3bar1;
    GameObject tank3bar2;
    GameObject tank3bar3;

    int script3count;
    int realcount;
    int rest;

    public int count5 = 0;

    // Start is called before the first frame update
    void Start()
    {
        tap = GameObject.Find("Tap");
        tank3 = GameObject.Find("Tank3");

        tank5bar1 = GameObject.Find("Tank5Bar1");
        tank5bar2 = GameObject.Find("Tank5Bar2");
        tank5bar3 = GameObject.Find("Tank5Bar3");
        tank5bar4 = GameObject.Find("Tank5Bar4");
        tank5bar4.GetComponent<Renderer>().material.color = Color.green;
        tank5bar5 = GameObject.Find("Tank5Bar5");

        tank3bar1 = GameObject.Find("Tank3Bar1");
        tank3bar2 = GameObject.Find("Tank3Bar2");
        tank3bar3 = GameObject.Find("Tank3Bar3");

        script3count = GameObject.Find("Tank3").GetComponent<WaterPuzzleTank3>().count3;
    }

    void Interact()
    {
        if(gameObject.GetComponent<Renderer>().material.color == Color.red)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tap.GetComponent<Renderer>().material.color == Color.red))
        {
            count5 = 5;
            tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
            tank5bar2.GetComponent<Renderer>().material.color = Color.yellow;
            tank5bar3.GetComponent<Renderer>().material.color = Color.yellow;
            tank5bar4.GetComponent<Renderer>().material.color = Color.yellow;
            tank5bar5.GetComponent<Renderer>().material.color = Color.yellow;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tap.GetComponent<Renderer>().material.color = Color.white;
        }

        if ((gameObject.GetComponent<Renderer>().material.color == Color.red) && (tank3.GetComponent<Renderer>().material.color == Color.red))
        {
            realcount = count5 + GameObject.Find("Tank3").GetComponent<WaterPuzzleTank3>().count3;

            if (realcount > 5)
            {
                rest = realcount - 5;
                realcount -= rest;
            }

            switch (realcount)
            {
                case 5:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 4:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    print("You win!");
                    break;
                case 3:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 2:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 1:
                    tank5bar5.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar4.GetComponent<Renderer>().material.color = Color.green;
                    tank5bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank5bar1.GetComponent<Renderer>().material.color = Color.yellow;
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

            count5 = realcount;

            GameObject.Find("Tank3").GetComponent<WaterPuzzleTank3>().count3 = rest;

            switch (rest)
            {
                case 3:
                    tank3bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank3bar3.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 2:
                    tank3bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.yellow;
                    tank3bar1.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 1:
                    tank3bar3.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar2.GetComponent<Renderer>().material.color = Color.white;
                    tank3bar1.GetComponent<Renderer>().material.color = Color.yellow;
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

            rest = 0;

            gameObject.GetComponent<Renderer>().material.color = Color.white;
            tank3.GetComponent<Renderer>().material.color = Color.white;

        }
    }
}
