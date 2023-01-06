using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] GameObject[] objectsToMove;
    [SerializeField] bool triggerOnce = true;
    bool triggered;
    bool isMoving = false;
    public void Move()
    {
        if (triggerOnce && !triggered)
        {
            triggered = true;
            StartMoving();
            return;
        }
        else if (!triggerOnce)
        {
            triggered = true;
            StartMoving();
            return;
        }
    }

    private void StartMoving()
    {
        isMoving = true;
    }
    private void Update()
    {
        if (isMoving)
        {
            foreach (GameObject movable in objectsToMove)
            {
                movable.transform.position += speed * direction * Time.deltaTime;
            }
        }
    }
}
