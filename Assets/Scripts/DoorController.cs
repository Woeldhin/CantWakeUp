using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float[] angles = { 0, 95 };
    public int openState;
    public GameObject parent;
    private float currentAngle;
    public bool locked;

    //Audio stuff//
    //Clips
    public AudioClip doorOpen;
    public AudioClip doorCloses;
    //Sources
    public AudioSource forOpeningDoor;
    public AudioSource forClosingDoor;

    // Start is called before the first frame update
    void Start()
    {
        //Audio stuff
        forOpeningDoor.clip = doorOpen;
        forClosingDoor.clip = doorCloses;

        openState = 0;
    }

    void Update()
    {
        if (parent.transform.localEulerAngles.y != angles[openState])
        {
            currentAngle = Mathf.Lerp(parent.transform.localEulerAngles.y, angles[openState], 0.05f);

            parent.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
        }
    }

    void OpenAndClose()
    {
        if (openState == 0)
        {
            //forOpeningDoor.Play();
            openState = 1;
            forOpeningDoor.Play();
        }
        else
        {
            //forClosingDoor.Play();
            openState = 0;
            forClosingDoor.PlayDelayed(0.4f);
        }
    }

    void Interact()
    {
        if (locked)
        {
            Debug.Log("It's locked.");
        }
        else
        {
            OpenAndClose();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Key"))
        {
            locked = !locked;
            collision.transform.gameObject.SetActive(false);
        }
    }
}
