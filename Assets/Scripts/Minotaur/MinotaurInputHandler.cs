using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurInputHandler : MonoBehaviour, IInputHandler, ICharacterManager
{

    IFighter minotaurFighter;
    IMovement minotaurMovement;
    RageController rageController;
    
    private void Awake()
    {
        minotaurFighter = GetComponent<IFighter>();
        minotaurMovement = GetComponent<IMovement>();
        rageController = GetComponent<RageController>();
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
        //rageController.IncreaseRage(rageController.primaryAttackPoints);
    }

    public void SpecialAttack()
    {
        minotaurFighter.RageAttack();
    }

    public IFighter GetCharacterFighter()
    {
        return minotaurFighter;
    }

    public IMovement GetCharacterMovement()
    {
        return minotaurMovement;
    }

    public RageController GetRageController()
    {
        return rageController;
    }
}
