using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurInputHandler : MonoBehaviour, IInputHandler
{
    MinotaurFighter minotaurFighter;
    MinotaurMovement minotaurMovement;
    private void Awake()
    {
        minotaurFighter = GetComponent<MinotaurFighter>();
        minotaurMovement = GetComponent<MinotaurMovement>();
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void Jump()
    {
        minotaurMovement.Jump();
    }

    public void MoveHorizontally(float horizontal)
    {
        minotaurMovement.MoveHorizontally(horizontal);
    }

    public void PrimaryAttack()
    {
        minotaurFighter.PrimaryAttack();
    }

    public void SpecialAttack()
    {
        minotaurFighter.SpecialAttack();
    }

}
