using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour, IFighter, IAttackable
{
    [SerializeField] private AttackType attackType;
    [SerializeField] private CharacterType characterType = CharacterType.Enemy;
    [SerializeField] int maxHealth = 150;
    [SerializeField] int currentHealth = 150;
    [SerializeField] private bool isRageBarFull = false;
    public void OnPrimaryAtttackLanded()
    {
        //throw new System.NotImplementedException();
    }

    public void OnRageBarFilled()
    {
       // throw new System.NotImplementedException();
    }

    public void PrimaryAttack()
    {
        //throw new System.NotImplementedException();
    }

    public void RageAttack()
    {
       // throw new System.NotImplementedException();
    }

    public AttackType GetAttackType()
    {
        return attackType;
       // throw new System.NotImplementedException();
    }

    public CharacterType GetCharacterType()
    {
        return characterType;
        //throw new System.NotImplementedException();
    }

    public void OnAttacked(CharacterType ctype, AttackType atype)
    {
        switch (atype)
        {
            case AttackType.Melee:
              
                break;

            case AttackType.Ranged:
               
                break;
        }
    }

    
   
}
