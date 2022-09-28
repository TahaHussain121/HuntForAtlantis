using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusAttacker : MonoBehaviour, IFighter, IAttackable
{

    //// Test Variables
    public Transform Target;
    ///
    [SerializeField] List<CerberusHead> headList;
    [SerializeField] private bool isRageBarFull = false;
    [SerializeField] private CerberusHead meleeHead;
    [SerializeField] private AttackType attackType;
    [SerializeField] private CharacterType characterType=CharacterType.Cerberus;

    private Animator anim;
    private ICharacterManager characterManager;

    private bool isAttacking = false;
    private bool isInvincible;
    private bool isMele=false;

    public delegate void Damage(int val);
    public static Damage TakeDamage;
    public void OnEnable()
    {
       
        CerberusHead.TakeAttack+= OnAttacked ;
    }
 
    void Start()
    {
        Target = Gamemanager.ActiveIInputHandler.GetTransform();
        anim = GetComponent<Animator>();
        characterManager = GetComponent<ICharacterManager>();
    }


    public bool CheckEnemyInRange() {

      

        if (headList[0].CheckRange() || headList[1].CheckRange() || headList[2].CheckRange())
        {
            Debug.Log("True");
            return true;
        }
        Debug.Log("False");

        return false;
    }
    public void PrimaryAttack()
    {
        if (!isRageBarFull&&!isMele)
           {
         StartRangeAttack();
         }
    }
    
    public void MeleeAttack()
    {
        if (!isRageBarFull)
           {
            StartMeleeAttack();
         }
    }

  
    public void SpecialAttack()
    {

    }

  
    private void StartMeleeAttack()
    {
        StopAllCoroutines();

        if (!isAttacking&&isMele)
        {
            isAttacking = true;
            attackType = AttackType.Melee;

            StartCoroutine(LungeAttack(meleeHead)); ;
        }
    }
    
   
    private void StartRangeAttack()
    {
        //StopAllCoroutines();
        if (!isAttacking)
        {
            isAttacking = true;
            attackType = AttackType.Ranged;
            StartCoroutine("RangeAttack");

           

        }
    }
    
    private IEnumerator LungeAttack(CerberusHead head)
    {
        yield return new WaitForSeconds(2);

        head.TakePosForLunge();
        yield return new WaitForSeconds(0.5f);

        head.LungeAttack();
        yield return new WaitForSeconds(1f);

        head.ResetPos();
        isMele = false;

        isAttacking = false;


    }
    public IEnumerator RangeAttack()
    {
        Target = Gamemanager.ActiveIInputHandler.GetTransform();
        List<CerberusHead> cb = new List<CerberusHead>();
        cb = ShuffleList(headList);
        FireBallAttack( Target, cb[0]);
        yield return new WaitForSeconds(0.5f);
        FireBallAttack( Target, cb[1]);
        yield return new WaitForSeconds(0.5f);
        FireBallAttack( Target, cb[2]);
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;


    }
    [ContextMenu("RageAttack")]
    public void RageAttack()
    {
        attackType = AttackType.Rage;
        StopAllCoroutines();
        StartCoroutine("Rage");
    }
    public IEnumerator Rage()
    {
        List<CerberusHead> cb = new List<CerberusHead>();
        cb = ShuffleList(headList);
        cb = ShuffleList(headList);

        cb[0].Shake();
        cb[1].Shake();
        cb[2].PullBack();
        yield return new WaitForSeconds(1f);
        cb[0].ResetPos();
        cb[1].ResetPos();
        yield return new WaitForSeconds(0.5f);
        cb[0].ThrowLaserbeam();
        cb[1].ThrowLaserbeam();
        yield return new WaitForSeconds(3f);
        cb[0].ResetPos();
        cb[1].ResetPos();
        cb[2].ResetPos();
        isAttacking = false;
        OnRageBarEmptied();

    }
    private void FireBallAttack( Transform target, CerberusHead head)
    {

        head.ThrowFireball( target);

    }


    public void OnRageBarEmptied()
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
    
     public void OnAttacked(CharacterType ctype,AttackType atype,CerberusHead head)
    {
        if (isInvincible) return;

        RageController rageController = characterManager.GetRageController();

        if (ctype == CharacterType.Minotaur)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    Debug.Log("Minataur melee");
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    TakeDamage(10);
                    MeleeAttack();
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    TakeDamage(10);
                    break;
            }

        }
        else if (ctype == CharacterType.Satyr)
        {
            switch (atype)
            {
                case AttackType.Melee:
                    rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                    TakeDamage(10);
                    break;

                case AttackType.Ranged:
                    rageController.IncreaseRage(rageController.attackedWithRangePoints);
                    TakeDamage(10);
                    break;
            }
        }
    }

    public void OnRageBarFilled()
    {
        isInvincible = true;
        Debug.Log("rage");
        RageAttack();
    } 
    
    private void OnDeath(CerberusHead head)
    {
        //Not implemente4d (first correct the hardcode attacks)
        headList.Remove(head);
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

   

    public void OnPrimaryAtttackLanded()
    {
        throw new System.NotImplementedException();
    }

    public AttackType GetAttackType()
    {
        return attackType;
    }
    public CharacterType GetCharacterType()
    {
        return characterType;
    }
}
