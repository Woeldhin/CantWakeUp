using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Holdable : MonoBehaviour
{
    public Rigidbody rb;
    public float pullForce = 100;
    private Vector3 velocity;
    private Vector3 lastFramePosition;
    private Vector3 destination;
    private bool held = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Interactable";
    }

    void Interact()
    {
        //rb.freezeRotation = true;
        lastFramePosition = transform.position;
        rb.useGravity = false;
        held = true;
    }

    void Hold(Vector3 grabPoint)
    {
        velocity = (transform.position - lastFramePosition) / (Time.deltaTime * 2);
        lastFramePosition = transform.position;
        //rb.AddForce((grabPoint - transform.position) * pullForce * Vector3.Distance(transform.position, grabPoint));
        /*if(Vector3.Distance(transform.position, grabPoint) < 0.05f)
        {
            rb.velocity *= Vector3.Distance(transform.position, grabPoint);
        }*/

        Ray ray = new Ray(transform.position, grabPoint - transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, grabPoint));

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform != null)
            {
                if (hits[i].transform != transform)
                {
                    destination = hits[i].point;
                    return;
                }
            }
            else
            {
                break;
            }
        }

        destination = grabPoint;

        //transform.position = grabPoint;
        //transform.LookAt(grabPoint);
    }

    void FixedUpdate()
    {
        if(held)
        {
            rb.velocity = ((destination - transform.position) * 10);
            rb.angularVelocity *= 0.9f;
        }
    }

    void Reset()
    {
        rb.velocity = velocity;
       // rb.freezeRotation = false;
        rb.useGravity = true;
        held = false;
    }
}
