using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoppinginputhandler : MonoBehaviour, IInputHandler
{
    Movement movement;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void Jump()
    {
        movement.Jump();
    }

    public void MoveHorizontally(float horizontal)
    {
        movement.MoveHorizontally(horizontal);
    }

    public void PrimaryAttack()
    {
        throw new System.NotImplementedException();
    }

    public void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
