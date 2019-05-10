using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClockButton : MonoBehaviour
{
    public GameObject alarmClock;

    public AudioSource AlarmSource;
    //Audio stuff//
    //AudioClips
    public AudioClip buttonSoundFail;
    public AudioClip buttonSoundSuccess;
    //AudioSources
    public AudioSource buttonSourceFail;
    public AudioSource buttonSourceSuccess;
    // Start is called before the first frame update
    void Start()
    {
        buttonSourceFail.clip = buttonSoundFail;
        buttonSourceSuccess.clip = buttonSoundSuccess;
    }
    
    // Update is called once per frame
    void Interact()
    {
        if (alarmClock.GetComponent<AlarmClockController>().minuteCounter == alarmClock.GetComponent<AlarmClockController>().minuteSuccess && alarmClock.GetComponent<AlarmClockController>().hourCounter == alarmClock.GetComponent<AlarmClockController>().hourSuccess)
        {
            gameObject.transform.localPosition = new Vector3(0, 0.477f, 0);
            buttonSourceSuccess.Play();
            Destroy(AlarmSource);
        }
        else
        {
            buttonSourceFail.Play();
        }
    }
}
