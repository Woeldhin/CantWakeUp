using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClockController : MonoBehaviour
{

    public GameObject minuteScroll;
    public GameObject hourScroll;

    public GameObject minutePointer;
    public GameObject hourPointer;

    public int hourCounter = 7;
    public int minuteCounter = 0;

    private float tiltAngle = -30.0f;

    void Interact()
    {
        print("Beep beep beep, beep beep beep!");
        if (gameObject == minuteScroll)
        {
            Debug.Log("MinutesTurning");
            minutePointer.transform.Rotate(0, tiltAngle, 0);
            if (minuteCounter < 11)
            {
                Debug.Log("MinutesTurning");
                minuteCounter++;
            }
            else
            {
                minuteCounter = 0;
            }
            
        }
        if (gameObject == hourScroll)
        {
            Debug.Log("HoursTurning");
            hourPointer.transform.Rotate(0, tiltAngle, 0);
            if (hourCounter < 11)
            {
                hourCounter++;
            }
            else
            {
                hourCounter = 0;
            }
        }
    }

    void Update()
    {
        //Debug.Log("Tunnit");
        //Debug.Log(hourCounter);
        //Debug.Log("Minuutit");
        //Debug.Log(minuteCounter);
        if (minuteCounter == 0 && hourCounter == 3)
        {
            Debug.Log("Voitit kellopelin!");
        }
    }
}
