using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurInputHandler : MonoBehaviour, IInputHandler, ICharacterManager
{
    IInteraction minotaurInteraction;
    IFighter minotaurFighter;
    IMovement minotaurMovement;
    RageController rageController;
    
    private void Awake()
    {
        minotaurFighter = GetComponent<IFighter>();
        minotaurMovement = GetComponent<IMovement>();
        minotaurInteraction = GetComponent<IInteraction>();

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
    }

    public void SpecialAttack()
    {
        minotaurFighter.RageAttack();
    }
    public void Interact()
    {
        minotaurInteraction.Interact();
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
