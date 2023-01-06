using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurFighter : MonoBehaviour, IFighter, IAttackable
{
    [SerializeField] float dashSpeed;
    [SerializeField] private bool isRageBarFull = false;
    [SerializeField] private float breakableBlockDistance;

    private Animator anim;
    private ICharacterManager characterManager;
    public bool isAttacking = false;
    public Vector3 directionFacing;
    private bool isInvincibleInRage;
    [SerializeField] private AttackType attackType;
    [SerializeField] private CharacterType characterType = CharacterType.Minotaur;
    //public bool IsInvincibleInRage { get => isInvincibleInRage; }

    private void Awake()
    {
        directionFacing = Vector3.right;
        anim = GetComponent<Animator>();
        characterManager = GetComponent<ICharacterManager>();
    }
    
    public void PrimaryAttack()
    {
        StartMeleeAttackAnim();
    }
    public void RageAttack()
    {
        if (isRageBarFull)
        {
            attackType = AttackType.Rage;

            StartDashAttackAnim();
        }
    }
    private void StartMeleeAttackAnim()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackType = AttackType.Melee;
            anim.SetTrigger("MeleeAttack");
            //print("Melee Attack");
        }
    }
    private void StartDashAttackAnim()
    {
        if(!isAttacking)
        {
            isAttacking = true;
            isInvincibleInRage = true;

            anim.SetTrigger("DashAttack");
            StartCoroutine("MoveForwardDuringDash");
        }
    }
    private IEnumerator MoveForwardDuringDash()
    {
        while (isInvincibleInRage)
        {
            yield return new WaitForEndOfFrame();

            transform.Translate(directionFacing * Time.deltaTime * dashSpeed, Space.World);
            //transform.Translate(1 * Time.deltaTime * dashSpeed, 0 , 0);

        }
    }
    public void OnMeleeAnimEnd() // called from animation event
    {
        isAttacking = false;
        CheckBreakableBlock();
    }

    private void CheckBreakableBlock()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, directionFacing, breakableBlockDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "BreakableBlock")
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void OnDashAnimEnd() // called from animation event
    {
        isAttacking = false;
        isInvincibleInRage = false;

        OnRageBarEmptied();
    }
  
    public void OnRageBarFilled()
    {
        isRageBarFull = true;
    }
    private void OnRageBarEmptied()
    {
        isRageBarFull = false;
        characterManager.GetRageController().ResetRage();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something Hit");

        IAttackable Attacker = collision.gameObject.GetComponent<IAttackable>();
        if (Attacker != null)
        {
            Debug.Log("Something Hit");
            OnAttacked(Attacker.GetCharacterType(), Attacker.GetAttackType());
        }
        else if (collision.gameObject.tag == "Cerberus")
        {
            Debug.Log("Arrow");

            OnAttacked(CharacterType.Cerberus, AttackType.Ranged);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("enemy");

            OnAttacked(CharacterType.Enemy, AttackType.Melee);

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cerberus")
        {
            Debug.Log("Arrow");

            OnAttacked(CharacterType.Cerberus, AttackType.Ranged);
        }
    }

    public void OnAttacked(CharacterType ctype, AttackType atype) // NOTE: what is the use for cType?
    {
        if (isInvincibleInRage) return;

        RageController rageController = characterManager.GetRageController();
        Health healthController = characterManager.GetHealthController();

        switch (atype)
        {
            case AttackType.Melee:
                
                rageController.IncreaseRage(rageController.attackedWithMeleePoints);
                healthController.TakeDamage(10);
                break;

            case AttackType.Ranged:
                rageController.IncreaseRage(rageController.attackedWithRangePoints);
                healthController.TakeDamage(10);
                break;
        }
    }
    public void OnPrimaryAtttackLanded()
    {
        characterManager.GetRageController().IncreaseRage(characterManager.GetRageController().primaryAttackPoints);
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
