using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClockController : MonoBehaviour
{
    // Drawer puzzle starter
    public DrawerController drawer;

    //Scrolls at back of clock that player uses to change clocks time
    public GameObject minuteScroll;
    public GameObject hourScroll;

    //Imaginary "pointer" that show us time, really just used as pivot points to originally check the rotation of pointers to solve this puzzle, now pretty useless, but it 'kay
    public GameObject minutePointer;
    public GameObject hourPointer;

    //manually set counters to check what time clock is showing right now , also needed to check if player has solved the puzzle
    public int hourCounter = 7;
    public int minuteCounter = 0;

    //how much will pointers tilt from one click of the "scrolls"
    private float tiltAngle = -30.0f;

    void Interact()
    {
        print("Beep beep beep, beep beep beep!");
        //turning of minute pointer and keeping minuteCounter at right number
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

        //turning of hour pointer and keeping hourCounter at right number
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

        //Check if minute and hour counters are at right position for player to "win" this puzzle
        if (minuteCounter == 0 && hourCounter == 3)
        {
            Debug.Log("Voitit kellopelin!");
            drawer.TogglePosition();
        }
    }
}
