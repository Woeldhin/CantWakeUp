using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePuzzleController : MonoBehaviour
{
    bool isTriangleDone;
    bool isSquareDone;
    bool isCircleDone;

    void Start()
    {

        isTriangleDone = false;
        isSquareDone = false;
        isCircleDone = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Check if triangle object exists
        if (GameObject.Find("TrianglePiece"))
        {
            isTriangleDone = false;
        }
        else
        {
            isTriangleDone = true;
        }

        //Check if square object exists
        if (GameObject.Find("SquarePiece"))
        {
            isSquareDone = false;
        }
        else
        {
            isSquareDone = true;
        }

        //Check if circle object exists
        if (GameObject.Find("CirclePiece"))
        {
            isCircleDone = false;
        }
        else
        {
            isCircleDone = true;
        }

        //Checks if the puzzle is solved fully (all 3 pieces put to place) = win the puzzle
        if (isTriangleDone == true && isSquareDone == true && isCircleDone == true)
        {
            Debug.Log("You won the shape puzzle");
        }
    }
}
