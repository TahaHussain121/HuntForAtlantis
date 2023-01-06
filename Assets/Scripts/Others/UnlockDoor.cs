using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField] bool triggerOnce = true;
    [SerializeField] Door pairedDoor;
    bool triggered;
    public void Unlock()
    {
        if (triggerOnce && !triggered)
        {
            triggered = true;
            pairedDoor.Unlock();
            return;
        }
        else if (!triggerOnce)
        {
            triggered = true;
            pairedDoor.Unlock();

            return;
        }
    }
}
