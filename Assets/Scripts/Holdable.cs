using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    void Hold(Vector3 grabPoint)
    {
        transform.position = grabPoint;
    }
}
