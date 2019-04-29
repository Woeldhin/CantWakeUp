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
    // parent
    DresserController parent;
    // Lerp time in seconds
    [SerializeField]
    float lerpTime = 0.5f;

    void Start()
    {
        // Set the closed position to be the initial position of the drawer
        closedPos = transform.localPosition.x;
        // Set the open position to be a position that's maxPull distance away from the closed position
        openPos = closedPos + maxPull;
        // set the current position to be the intial world position of the drawer
        currentPoint = transform.localPosition;
        // set parent
        parent = this.gameObject.GetComponentInParent<DresserController>();
        // If the starting state was set as open, move the drawer accordingly
        if (isOpen)
        {
            currentPoint.x = openPos;
            transform.localPosition = currentPoint;
        }
    }

    // Alternative for Hold, faster for testing
    /*void Interact()
    {
        TogglePosition();
        CallParent();
    }*/

    // Do this every frame the player is holding the hold button on the drawer
    void Hold(Vector3 grabPoint)
    {
        // Transform grabPoint into local coordinates
        transform.InverseTransformPoint(grabPoint);

        // Check if difference has been set
        if (difference == 0)
        {
            // Set the difference as the distance between the grabpoint and transform position on the z axis
            difference = transform.localPosition.x - grabPoint.z;
        }

        // Check if the potential new position for the drawer is between the closed and open positions
        if (grabPoint.z + difference >= closedPos && grabPoint.z + difference <= openPos)
        {
            // Set the current position to align with the current position of grabpoint
            currentPoint.x = grabPoint.z + difference;
            // Set new position for transform
            transform.localPosition = currentPoint;
        }
    }

    // Called when the hold is released, snap to closer position
    private void Reset()
    {
        // Reset difference to 0
        difference = 0;

        // Jump to open position if it's closer then half the maxPull
        if(Mathf.Abs(currentPoint.x - closedPos) > maxPull/2)
        {
            if (!isOpen)
            {
                isOpen = true;
                CallParent();
            }
        }
        // Otherwise jump to closed position
        else
        {
            if (isOpen)
            {
                isOpen = false;
                CallParent();
            }
        }

        // Update transform
        StartCoroutine(LerpToPosition());
    }

    // Let the DresserParent know that we moved
    void CallParent()
    {
        parent.DrawerUpdate(this);
    }

    // Toggle open state and position
    public void TogglePosition()
    {
        // toggle isOpen
        isOpen = !isOpen;

        // Update transform
        StartCoroutine(LerpToPosition());
    }

    IEnumerator LerpToPosition()
    {
        // Is already at desired position
        if((isOpen && currentPoint.z == openPos) || (!isOpen && currentPoint.x == closedPos))
        {
            yield return null;
        }

        float startPos = transform.localPosition.x;
        float finishPos;

        if(isOpen)
        {
            finishPos = openPos;
        } else
        {
            finishPos = closedPos;
        }

        float elapsedTime = 0;

        // Lerp into desired position
        while(elapsedTime < lerpTime)
        {
            currentPoint.x = Mathf.Lerp(startPos, finishPos, (elapsedTime / lerpTime));
            transform.localPosition = currentPoint;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }
}
