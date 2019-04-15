using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPuzzleReset : MonoBehaviour

{
    GameObject tap;
    GameObject tank3;
    GameObject tank5;


    Color color = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        tap = GameObject.Find("Tap");
        tank5 = GameObject.Find("Tank5");
        tank3 = GameObject.Find("Tank3");
    }

    void Interact()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("WaterPuzzleBars");
        foreach (GameObject go in objects)
        {
            Renderer[] renderers = go.GetComponents<Renderer>();
            foreach (Renderer r in renderers)
            {
                foreach (Material m in r.materials)
                {
                    if (m.HasProperty("_Color"))
                        m.color = color;
                }
            }
        }

        GameObject.Find("Tank5").GetComponent<WaterPuzzleTank5>().count5 = 0;
        GameObject.Find("Tank3").GetComponent<WaterPuzzleTank3>().count3 = 0;

        tap.GetComponent<Renderer>().material.color = Color.white;
        tank3.GetComponent<Renderer>().material.color = Color.white;
        tank5.GetComponent<Renderer>().material.color = Color.white;

    }
         

}
