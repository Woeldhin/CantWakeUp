using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    // Objects telling the drawer the direction and length of movement
    public Transform stop;
    public Transform center;
    // Starting position of drawer
    public Vector3 startPos;
    // The closed position of the drawer
    public float closedPos;
    // Fully open position of the drawer
    public float openPos;
    // Vector of movement
    public Vector3 direction;
    // Distance on the z axis between the player's grab point and drawer position
    public float difference;
    // Current location of the drawer
    public float currentPoint;
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
        // Starting position of the drawer
        startPos = transform.position;
        // Allowed direction of movement
        direction = Vector3.Normalize(center.position - stop.position);
        // Maximum distance of movement
        maxPull = Vector3.Magnitude(center.position - stop.position);
        // Set the closed position to be the initial position of the drawer
        closedPos = 0;
        // Set the open position to be a position that's maxPull distance away from the closed position
        openPos = closedPos + maxPull;
        // set the current position to be the intial world position of the drawer
        currentPoint = closedPos;
        // set parent
        parent = this.gameObject.GetComponentInParent<DresserController>();
        // If the starting state was set as open, move the drawer accordingly
        if (isOpen)
        {
            currentPoint = openPos;
            transform.position = (startPos + (direction * (openPos - closedPos)));
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

        // Check if difference has been set
        if (difference == 0)
        {
            // Set the difference as the distance between the grabpoint and transform position
            difference = Vector3.Dot(grabPoint - transform.position, direction);
        }

        // Check if the potential new position for the drawer is between the closed and open positions
        Debug.Log(Vector3.Dot(grabPoint - startPos, direction));
        if (Vector3.Dot(grabPoint - startPos, direction) - difference >= closedPos && Vector3.Dot(grabPoint - startPos, direction) - difference <= openPos)
        {
            // Set the current position to align with the current position of grabpoint
            currentPoint = Vector3.Dot(grabPoint - startPos, direction) - difference;
            // Set new position for transform
            transform.position = (startPos + (direction * currentPoint));
        }
    }

    // Called when the hold is released, snap to closer position
    private void Reset()
    {
        // Reset difference to 0
        difference = 0;

        // Jump to open position if it's closer then half the maxPull
        if(Mathf.Abs(currentPoint - closedPos) > maxPull/2)
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
        if((isOpen && currentPoint == openPos) || (!isOpen && currentPoint == closedPos))
        {
            yield return null;
        }

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
            currentPoint = Mathf.Lerp(currentPoint, finishPos, (elapsedTime / lerpTime));
            transform.position = (startPos + (direction * currentPoint));
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
    }
}
