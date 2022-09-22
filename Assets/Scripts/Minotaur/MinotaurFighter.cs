using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurFighter : MonoBehaviour, IFighter, IAttackable
{
    [SerializeField] int maxHealth = 150;
    [SerializeField] int currentHealth = 150;
    [SerializeField] private bool isRageBarFull = false;             

    private Animator anim;
    private ICharacterManager characterManager;
    public bool isAttacking = false;
    public Vector3 directionFacing;
    private bool isInvincible;

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
    public void SpecialAttack()
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
            isInvincible = true;

            anim.SetTrigger("DashAttack");
            // dash attack
        }
    }
    public void OnMeleeAnimEnd() // called from animation event
    {
        isAttacking = false;
    }
    public void OnDashAnimEnd() // called from animation event
    {
        isAttacking = false;
        isInvincible = false;

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
        if (isInvincible) return;

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
}
