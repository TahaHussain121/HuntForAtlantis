using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    [SerializeField] bool triggerOnce = true;
    bool triggered;
    public void Switch()
    {
        if (triggerOnce && !triggered)
        {
            triggered = true;
            Gamemanager.SwitchCharacter();
            return;
        }
        else if (!triggerOnce)
        {
            triggered = true;
            Gamemanager.SwitchCharacter();
            return;
        }
    }
}
