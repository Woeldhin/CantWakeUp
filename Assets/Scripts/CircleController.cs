using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "CirclePiece")
        {
            Debug.Log("CircleHit");
            Destroy(col.gameObject);
        }
    }
}
