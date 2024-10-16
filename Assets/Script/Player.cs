using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isMoving; //keeps the player from making multiple movement commands at once
    private Vector2 origPos, targetPos;
    private float timeToMove = 0.2f;


    //public float speed = 5;


    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving)
            StartCoroutine(MovePlayer(Vector2.up));

        if (Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(Vector2.left));

        if (Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(Vector2.down));

        if (Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(Vector2.right));

    }

    private IEnumerator MovePlayer(Vector2 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
        transform.position = Vector2.Lerp(origPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime = Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

}

