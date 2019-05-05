using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 velocity;
    private Vector3 lastFramePosition;

    void Interact()
    {
        rb.freezeRotation = true;
        lastFramePosition = transform.position;
    }

    void Hold(Vector3 grabPoint)
    {
        velocity = (transform.position - lastFramePosition) / (Time.deltaTime * 2);
        lastFramePosition = transform.position;
        transform.position = grabPoint;
        transform.LookAt(grabPoint);
    }

    void Reset()
    {
        rb.velocity = velocity;
        rb.freezeRotation = false;
    }
}
