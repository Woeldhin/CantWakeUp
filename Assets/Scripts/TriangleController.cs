using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "TrianglePiece")
        {
            Debug.Log("TriangleHit");
            Destroy(col.gameObject);
        }
    }
}
