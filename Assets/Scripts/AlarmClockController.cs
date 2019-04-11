using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClockController : MonoBehaviour
{

    public GameObject minuteScroll;
    public GameObject hourScroll;

    public GameObject minutePointer;
    public GameObject hourPointer;

    private float tiltAngle = -30.0f;

    void Interact()
    {
        print("Beep beep beep, beep beep beep!");
        if (gameObject == minuteScroll)
        {
            minutePointer.transform.Rotate(0, tiltAngle, 0);
        }
        if (gameObject == hourScroll)
        {
            hourPointer.transform.Rotate(0, tiltAngle, 0);
        }
    }

    void Update()
    {
        if(minutePointer.transform.localRotation.y == 0 && hourPointer.transform.localRotation.y == 120)
        {
            Debug.Log("Voitit kellopelin!");
        }
    }
}
