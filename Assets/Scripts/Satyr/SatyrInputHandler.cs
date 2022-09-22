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
    }

    public void SpecialAttack()
    {
        satyrFighter.RageAttack();
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

    public void Interact()
    {
        Debug.LogError("Interact ability for satyr has not been implemented yet.");
    }
}
