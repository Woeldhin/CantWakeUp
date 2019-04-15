using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DresserController : MonoBehaviour
{

    [SerializeField]
    // Array of the drawers that are part of the dresser
    DrawerController[] drawers;

    // Run every time drawers state is updated
    public void DrawerUpdate(DrawerController caller)
    {
        // Check if there is need to update other drawers
        for(int i = 0; i < drawers.Length; i++)
        {
            if(caller == drawers[i])
            {
                Debug.Log("match found!");
                DrawerMoved(i);
            }
        }
        CheckComplete();
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

        if (counter == drawers.Length)
        {
            Debug.Log("The DrawerPuzzle is Complete!");
            Destroy(this.gameObject);
        }
    }

    void DrawerMoved(int index)
    {
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
