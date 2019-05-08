using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DresserController : MonoBehaviour
{
    public Rigidbody key;
    public bool puzzleActive;

    [SerializeField]
    // Array of the drawers that are part of the dresser
    DrawerController[] drawers;
    [SerializeField]
    // Complete sound
    Sound victorySound;

    // Run every time drawers state is updated
    public void DrawerUpdate(DrawerController caller)
    {
        if(puzzleActive)
        {
            // Find the drawer from the array
            for (int i = 0; i < drawers.Length; i++)
            {
                if (caller == drawers[i])
                {
                    DrawerMoved(i);
                }
            }

            CheckComplete();
        }
    }

    // Check if the puzzle is completed
    void CheckComplete()
    {
        int counter = 0;
        // Check if all the drawers are open
        foreach (DrawerController drawer in drawers)
        {
            if (drawer.isOpen)
            {
                counter++;
            }
            else
            {
                break;
            }
        }
        // If all drawers are open, delete the dresser
        if (counter == drawers.Length)
        {
            if(victorySound != null)
            {
                victorySound.source = gameObject.AddComponent<AudioSource>();
                victorySound.source.clip = victorySound.clip;

                victorySound.source.volume = victorySound.volume;
                victorySound.source.pitch = victorySound.pitch;
                victorySound.source.loop = victorySound.loop;
                victorySound.source.Play();
            }
            key.isKinematic = false;
            key.useGravity = false;
            Destroy(this.gameObject, 1);
        }
    }

    // drawer moved so others may have to as well
    void DrawerMoved(int index)
    {
        // Cases for different sized dressers
        if(drawers.Length == 2)
        {
            switch(index)
            {
                case 0:
                    drawers[1].TogglePosition();
                    break;
                case 1:
                    drawers[0].TogglePosition();
                    break;
                default:
                    Debug.Log("DrawerMoved: Index Out Of Range! Length: 2");
                    break;
            }
        }

        if (drawers.Length == 3)
        {
            switch (index)
            {
                case 0:
                    drawers[1].TogglePosition();
                    break;
                case 1:
                    drawers[2].TogglePosition();
                    break;
                case 2:
                    drawers[0].TogglePosition();
                    drawers[1].TogglePosition();
                    break;
                default:
                    Debug.Log("DrawerMoved: Index Out Of Range! Length: 3");
                    break;
            }
        }

        CheckComplete();
    }

}
