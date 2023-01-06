using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool triggerOnce = true;
    [SerializeField] public Door pairedDoor;
    public bool triggered;
    public bool unlocked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!unlocked) return;
            if (triggerOnce && !triggered)
            {
                print("door triggered");
                triggered = true;
                TeleportCharacter(other);
                return;
            }
            else if (!triggerOnce)
            {
                triggered = true;
                TeleportCharacter(other);

                return;
            }
        }
    }

    private void TeleportCharacter(Collider other)
    {
        other.GetComponent<CharacterController>().enabled = false;
        //other.transform.position = pairedDoor.transform.position + Vector3.up * 2;
        other.transform.position = pairedDoor.transform.position + Vector3.up * 10;

        other.GetComponent<CharacterController>().enabled = true;

    }

    public void Unlock()
    {
        unlocked = true;
        pairedDoor.unlocked = true;
    }
}
