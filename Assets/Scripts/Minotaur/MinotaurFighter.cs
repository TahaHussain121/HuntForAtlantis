using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurFighter : MonoBehaviour
{
    [SerializeField] int maxHealth = 150;
    [SerializeField] int currentHealth = 150;
    [SerializeField] private bool isRageBarFull = false;

    private Animator anim;
    public bool isAttacking = false;

    public Vector3 directionFacing;
    private void Awake()
    {
        directionFacing = Vector3.right;
        anim = GetComponent<Animator>();
    }

    internal void PrimaryAttack()
    {
        MeleeAttack();
    }

    internal void SpecialAttack()
    {
        if (isRageBarFull)
        {
            DashAttack();
        }
        isRageBarFull = false;

    }

    private void DashAttack()
    {
        if(!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("DashAttack");
            // dash attack
        }
    }

    private void MeleeAttack()
    {
        if (!isAttacking)
        {
            print("Melee Attack");
            isAttacking = true;
            anim.SetTrigger("MeleeAttack");
        }
    }
    public void EndMeleeAttack()
    {
        isAttacking = false;
    }
    public void EndDashAttack()
    {
        isAttacking = false;
    }
    public void HealHealthByPercentage(float percentage)
    {
        currentHealth = Mathf.Clamp(currentHealth += Mathf.RoundToInt(currentHealth * (percentage / 100)), 0, maxHealth);
    }
}
