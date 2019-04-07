using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{

    private float minPull;
    private float maxPull;
    private float oldTangent;
    public float pull;
    public float currentPoint;

    void Start()
    {
        minPull = transform.position.z;
        maxPull = minPull - pull;
        oldTangent = 0;
    }

    void Hold(Vector3 lookPoint)
    {
        if (oldTangent != 0)
        {
            // Shrug

            if (currentPoint <= minPull && currentPoint >= maxPull)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentPoint);
            }
        }
        else
        {
            oldTangent = lookPoint.x - transform.position.x;
        }
        
    }

}
