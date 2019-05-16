using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Holdable : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 destination;
    private bool held = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Interactable";
    }

    void Interact()
    {
        rb.useGravity = false;
        held = true;
        gameObject.layer = 10;
    }

    void Hold(Vector3 grabPoint)
    {

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
        rb.useGravity = true;
        held = false;
        gameObject.layer = 0;
    }
}
