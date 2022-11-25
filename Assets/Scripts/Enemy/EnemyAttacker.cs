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
    private Coroutine _attackCoroutine;

    public bool CheckRange(Transform target)
    {
        Debug.Log("check range");

        if (Vector3.Distance(transform.position, target.position) < 10)
        {

    
                return true;

        }
        return false;
    }
    public void OnPrimaryAtttackLanded()
    {
        //throw new System.NotImplementedException();
    }

    public void OnRageBarFilled()
    {
       // throw new System.NotImplementedException();
    }

    public void PrimaryAttack(Transform target)
    {// change it to corutine please
        _attackCoroutine = StartCoroutine(MeleAttack(target));
        
    } 
    public void EndPrimaryAttack()
    {
        StopCoroutine(_attackCoroutine); 
        
    }

    private IEnumerator MeleAttack(Transform target)
    {
        while (true)
        {

            if (CheckRange(target))
            {
                Debug.Log("attack");
                //attack animaiton
            }
            yield return null;
        }



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

    public void PrimaryAttack()
    {
       // throw new System.NotImplementedException();
    }
}
