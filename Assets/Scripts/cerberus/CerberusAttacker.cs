using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusAttacker : MonoBehaviour, IFighter, IAttackable
{

    //// Test Variables
    public Transform TestObject;
    ///
    [SerializeField] List<CerberusHead> headList;
    [SerializeField] private bool isRageBarFull = false;

    private Animator anim;
    private ICharacterManager characterManager;

    private bool isAttacking = false;
    private bool isInvincible;

    public void OnEnable()
    {
        CerberusHead.TakeAttack+= OnAttacked ;
    }
    public void OnDisable()
    {
        
    }
    void Start()
    {
      
        anim = GetComponent<Animator>();
        characterManager = GetComponent<ICharacterManager>();
    }



    public void PrimaryAttack()
    {
        if (!isRageBarFull)
           {
        StartRangeAttack();
         }
    }

  
    public void SpecialAttack()
    {

    }
    private void StartMeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;


        }
    }
    private void StartRangeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            StartCoroutine("RangeAttack");

        }
    }

    public IEnumerator RangeAttack()
    {
        List<CerberusHead> cb = new List<CerberusHead>();
        cb = ShuffleList(headList);
        FireBallAttack( TestObject, cb[0]);
        yield return new WaitForSeconds(1f);
        FireBallAttack( TestObject, cb[1]);
        yield return new WaitForSeconds(1f);
        FireBallAttack( TestObject, cb[2]);
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;


    }
    private void FireBallAttack( Transform target, CerberusHead head)
    {

        head.ThrowFireball( target);

    }


    private void OnRageBarEmptied()
    {
        isRageBarFull = false;
        characterManager.GetRageController().ResetRage();
    }
    public void OnAttacked(CharacterType ctype,AttackType atype)
    {
        if (isInvincible) return;

        RageController rageController = characterManager.GetRageController();

        if (ctype == CharacterType.Minotaur)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    break;
            }

        }
        else if (ctype == CharacterType.Satyr)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    break;
            }
        }
    }

    public void OnRageBarFilled()
    {
        isInvincible = true;
    }

    
    private  List<CerberusHead> ShuffleList(List<CerberusHead> givenList)
    {

        for (int i = 0; i < givenList.Count; i++)
        {
            CerberusHead temp = givenList[i];
            int randomIndex = Random.Range(i, givenList.Count);
            givenList[i] = givenList[randomIndex];
            givenList[randomIndex] = temp;
        }

        return givenList;



    }
}
