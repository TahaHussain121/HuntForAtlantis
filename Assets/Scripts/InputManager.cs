using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Gamemanager.SwitchCharacter();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Gamemanager.ActiveIInputHandler.Jump();
            return;
        }

        if (Gamemanager.ActiveIInputHandler == null)
        {
            print("Gamemanager.ActiveIInputHandler == null ");
        }
        Gamemanager.ActiveIInputHandler.MoveHorizontally(Input.GetAxis("Horizontal"));
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Gamemanager.ActiveIInputHandler.PrimaryAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Gamemanager.ActiveIInputHandler.SpecialAttack();
        }

    }
}
