using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlarmClockController : MonoBehaviour
{
    // Drawer puzzle starter
    public DresserController dresser;
    public DrawerController drawer;

    //Imaginary "pointer" that show us time, really just used as pivot points to originally check the rotation of pointers to solve this puzzle, now pretty useless, but it 'kay
    public GameObject minutePointer;
    public GameObject hourPointer;

    public GameObject correctMinutePointer;
    public GameObject correctHourPointer;

    //manually set counters to check what time clock is showing right now , also needed to check if player has solved the puzzle
    private int hourCounter;
    private int minuteCounter;

    public AudioSource AlarmSource;
    //Audio stuff//
    //AudioClips
    public AudioClip buttonSoundFail;
    public AudioClip buttonSoundSuccess;
    //AudioSources
    public AudioSource buttonSourceFail;
    public AudioSource buttonSourceSuccess;

    [HideInInspector]
    public int hourSuccess;
    [HideInInspector]
    public int minuteSuccess;

    //how much will pointers tilt from one click of the "scrolls"
    private float tiltAngle = -30.0f;

    private void Start()
    {
        minuteCounter = Random.Range(0, 11);
        hourCounter = Random.Range(0, 11);
        minuteSuccess = Random.Range(0, 11);
        hourSuccess = Random.Range(0, 11);

        correctHourPointer.transform.localEulerAngles = new Vector3(0, 0, hourSuccess * tiltAngle);
        correctMinutePointer.transform.localEulerAngles = new Vector3(0, 0, minuteSuccess * tiltAngle);

        hourPointer.transform.localEulerAngles = new Vector3(0, hourCounter * tiltAngle);
        minutePointer.transform.localEulerAngles = new Vector3(0, minuteCounter * tiltAngle);

        buttonSourceFail.clip = buttonSoundFail;
        buttonSourceSuccess.clip = buttonSoundSuccess;
    }

    public void ButtonFeed(AlarmButtonType alarmButtonType)
    {
        switch (alarmButtonType)
        {
            case AlarmButtonType.minute:
                if (minuteCounter < 11)
                {
                    minuteCounter++;
                }
                else
                {
                    minuteCounter = 0;
                }
                minutePointer.transform.localEulerAngles = new Vector3(0, minuteCounter * tiltAngle);
                break;
            case AlarmButtonType.hour:
                if (hourCounter < 11)
                {
                    hourCounter++;
                }
                else
                {
                    hourCounter = 0;
                }
                hourPointer.transform.localEulerAngles = new Vector3(0, hourCounter * tiltAngle);
                break;
            case AlarmButtonType.button:
                //Check if minute and hour counters are at right position for player to "win" this puzzle
                if (minuteCounter == minuteSuccess && hourCounter == hourSuccess)
                {
                    Debug.Log("Voitit kellopelin!");
                    drawer.TogglePosition();
                    dresser.puzzleActive = true;
                    buttonSourceSuccess.Play();
                    Destroy(AlarmSource);
                } else
                {
                    buttonSourceFail.Play();
                }
                break;
            default:
                break;
        }
    }

}
