using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurInteraction : MonoBehaviour, IInteraction
{
    private bool isInContactWithPickableBlock;
    private Transform pickableBlock;
    private bool isInteracting;
    public void Interact()
    {
        if (isInteracting)
        {
            StopInteraction();
        }
        else
        {
            StartInteraction();
        }
    }

    private void StopInteraction()
    {
        PlaceBlock();
        isInteracting = false;
    }
    private void StartInteraction()
    {
        PickupBlock();
        isInteracting = true;
    }

    private void PlaceBlock()
    {
        pickableBlock.parent = null;
        //pickableBlock.GetComponent<Rigidbody>().useGravity = true;
        //pickableBlock.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void PickupBlock()
    {
        if (isInContactWithPickableBlock)
        {
            //pickableBlock.GetComponent<Rigidbody>().useGravity = false;
            pickableBlock.parent = transform;
            //pickableBlock.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickableBlock")
        {
            isInContactWithPickableBlock = true;
            pickableBlock = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickableBlock")
        {
            isInContactWithPickableBlock = false;
            pickableBlock = null;

        }
    }
}
