using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurFighter : MonoBehaviour, IFighter, IAttackable
{
    [SerializeField] int maxHealth = 150;
    [SerializeField] int currentHealth = 150;
    [SerializeField] float dashSpeed;
    [SerializeField] private bool isRageBarFull = false;

    private Animator anim;
    private ICharacterManager characterManager;
    public bool isAttacking = false;
    public Vector3 directionFacing;
    private bool isInvincibleInRage;

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
            StartDashAttackAnim();
        }
    }
    private void StartMeleeAttackAnim()
    {
        if (!isAttacking)
        {
            isAttacking = true;

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
    }
    public void OnDashAnimEnd() // called from animation event
    {
        isAttacking = false;
        isInvincibleInRage = false;

        OnRageBarEmptied();
    }
    public void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);
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
    public void OnAttacked(CharacterType ctype, AttackType atype) // NOTE: what is the use for cType?
    {
        if (isInvincibleInRage) return;

        RageController rageController = characterManager.GetRageController();

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
    public void OnPrimaryAtttackLanded()
    {
        characterManager.GetRageController().IncreaseRage(characterManager.GetRageController().primaryAttackPoints);
    }

}
