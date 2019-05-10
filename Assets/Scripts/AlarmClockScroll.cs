using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AlarmButtonType
{
    minute, hour, button
}

public class AlarmClockScroll : MonoBehaviour
{
    public AlarmButtonType alarmButtonType;
    private AlarmClockController alarmClockController;

    private void Start()
    {
        transform.tag = "Interactable";
        alarmClockController = GetComponentInParent<AlarmClockController>();
    }

    void Interact()
    {
        alarmClockController.ButtonFeed(alarmButtonType);
    }


}
