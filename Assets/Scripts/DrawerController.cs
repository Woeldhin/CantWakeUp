using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    // The closed position of the drawer
    public float closedPos;
    // Fully open position of the drawer
    public float openPos;
    // Distance on the z axis between the player's grab point and drawer position
    public float difference;
    // Current location of the drawer
    public Vector3 currentPoint;
    // Maximum distance to pull the drawer
    public float maxPull;
    // Indigator of current state
    public bool isOpen = false;

    void Start()
    {
        // Set the closed position to be the initial position of the drawer
        closedPos = transform.localPosition.z;
        // Set the open position to be a position that's maxPull distance away from the closed position
        openPos = closedPos - maxPull;
        // set the current position to be the intial world position of the drawer
        currentPoint = transform.localPosition;
        
        if(isOpen)
        {
            currentPoint.z = openPos;
            transform.localPosition = currentPoint;
        }
    }

    void Interact()
    {
        TogglePosition();
        CallParent();
    }

    // Do this every frame the player is holding the hold button on the drawer
    void Hold(Vector3 grabPoint)
    {
        // Transform grabPoint into local coordinates
        transform.InverseTransformPoint(grabPoint);

        // Check if difference has been set
        if (difference == 0)
        {
            // Set the difference as the distance between the grabpoint and transform position on the z axis
            difference = transform.localPosition.z - grabPoint.z;
        }

        // Check if the potential new position for the drawer is between the closed and open positions
        if (grabPoint.z + difference <= closedPos && grabPoint.z + difference >= openPos)
        {
            // Set the current position to align with the current position of grabpoint
            currentPoint.z = grabPoint.z + difference;
            // Set new position for transform
            transform.localPosition = currentPoint;
        }
    }

    // Called when the hold is released
    private void Reset()
    {

        // Reset difference to 0
        difference = 0;

        // Jump to open position if it's closer then half the maxPull
        if(Mathf.Abs(currentPoint.z - closedPos) > maxPull/2)
        {
            currentPoint.z = openPos;
            if (!isOpen)
            {
                isOpen = true;
                CallParent();
            }
        }
        // Otherwise jump to closed position
        else
        {
            currentPoint.z = closedPos;
            if (isOpen)
            {
                isOpen = false;
                CallParent();
            }
        }

        // Update transform
        transform.localPosition = currentPoint;
    }

    void CallParent()
    {
        this.gameObject.GetComponentInParent<DresserController>().DrawerUpdate(this);
    }

    public void TogglePosition()
    {
        isOpen = !isOpen;

        if(isOpen)
        {
            currentPoint.z = openPos;
        } else
        {
            currentPoint.z = closedPos;
        }

        transform.localPosition = currentPoint;
    }
}
