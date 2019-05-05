using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SquarePiece")
        {
            Debug.Log("SquareHit");
            Destroy(col.gameObject);
        }
    }
}
