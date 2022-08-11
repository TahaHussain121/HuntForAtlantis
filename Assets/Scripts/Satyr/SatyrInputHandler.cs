using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrInputHandler : MonoBehaviour, IInputHandler,IAttackable
{
    SatyrFighter satyrFighter;
    SatyrMovement satyrMovement;
    RageController rageBar;

    private void Awake()
    {
        satyrFighter = GetComponent<SatyrFighter>();
        satyrMovement = GetComponent<SatyrMovement>();
        rageBar = GetComponent<RageController>();
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
        rageBar.IncreaseRage(3);
    }

    public void SpecialAttack()
    {
        satyrFighter.SpecialAttack();
    }

    
    public void OnAttacked(CharacterType ctype, AttackType atype)
    {
        switch (atype)
        {
            case AttackType.Melee:
                rageBar.IncreaseRage(10);
                break;
            case AttackType.Ranged:
                rageBar.IncreaseRage(5);

                break;
        }

    }
}
