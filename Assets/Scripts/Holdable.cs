using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public Rigidbody rb;
    public float pullForce;
    private Vector3 velocity;
    private Vector3 lastFramePosition;

    void Interact()
    {
        rb.freezeRotation = true;
        lastFramePosition = transform.position;
        rb.useGravity = false;
    }

    void Hold(Vector3 grabPoint)
    {
        velocity = (transform.position - lastFramePosition) / (Time.deltaTime * 2);
        lastFramePosition = transform.position;
        rb.AddForce((grabPoint - transform.position) * pullForce * Vector3.Distance(transform.position, grabPoint));
        if(Vector3.Distance(transform.position, grabPoint) < 0.05f)
        {
            rb.velocity *= Vector3.Distance(transform.position, grabPoint);
        }
        
        //transform.position = grabPoint;
        //transform.LookAt(grabPoint);
    }

    void Reset()
    {
        rb.velocity = velocity;
        rb.freezeRotation = false;
        rb.useGravity = true;
    }
}
