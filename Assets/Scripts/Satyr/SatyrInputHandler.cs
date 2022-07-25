using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrInputHandler : MonoBehaviour, IInputHandler
{
    SatyrFighter satyrFighter;
    SatyrMovement satyrMovement;

    private void Awake()
    {
        satyrFighter = GetComponent<SatyrFighter>();
        satyrMovement = GetComponent<SatyrMovement>();
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void Jump()
    {
        satyrMovement.Jump();
    }

    public void MoveHorizontally(float horizontal)
    {
        satyrMovement.MoveHorizontally(horizontal);
    }

    public void PrimaryAttack()
    {
        satyrFighter.PrimaryAttack();
    }

    public void SpecialAttack()
    {
        satyrFighter.SpecialAttack();
    }
}
