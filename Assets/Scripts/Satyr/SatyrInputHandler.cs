using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrInputHandler : MonoBehaviour, IInputHandler,ICharacterManager
{
    IFighter satyrFighter;
    IMovement satyrMovement;
    RageController rageController;

    private void Awake()
    {
        satyrFighter = GetComponent<IFighter>();
        satyrMovement = GetComponent<IMovement>();
        rageController = GetComponent<RageController>();
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
        rageController.IncreaseRage(rageController.primaryAttackPoints);
    }

    public void SpecialAttack()
    {
        satyrFighter.SpecialAttack();
    }

    public IFighter GetCharacterFighter()
    {
        return satyrFighter;
    }

    public IMovement GetCharacterMovement()
    {
        return satyrMovement;
    }

    public RageController GetRageController()
    {
        return rageController;
    }
}
