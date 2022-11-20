using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrFighter : MonoBehaviour, IFighter, IAttackable
{
    [SerializeField] GameObject rangeProjectile;
    [SerializeField] GameObject bow;
    [SerializeField] private float projectileSpeed;
    [SerializeField] int maxAmmo = 15;
    [SerializeField] int currentAmmo = 15;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth = 100;

    [SerializeField] private bool isRageBarFull = false;

    private Animator anim;
    private ICharacterManager characterManager;
    public bool isAttacking;
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
        StartArrowShootingAnim();
    }
    public void RageAttack()
    {
        if (isRageBarFull)
        {
            StartArrowSpawningAnim();
        }
    }
    private void StartArrowShootingAnim()
    {
        if (currentAmmo > 0 && !isAttacking)
        {
            isAttacking = true;

            if (directionFacing == Vector3.right)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            }
            else if (directionFacing == Vector3.left)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            }

            RemoveAmmo(1);

            anim.SetTrigger("ShootArrow");
            //print("Range attack");
        }
    }
    private void StartArrowSpawningAnim()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            anim.SetTrigger("SpawnArrows");
            //print("SpecialRangeAttack");
        }
    }
    public void OnArrowShootingAnimEnd() // called from animation event
    {
        print("OnArrowShootingAnimEnd() called");
        Invoke("RotateBack", 0.5f);
        ShootProjectile();
        isAttacking = false;
    } 
    public void OnArrowSpawningAnimEnd() // called from animation event
    {
        //print("OnSpecialRangeAttackEnd() called");
        ArrowSpawner.SpawnArrows();
        isAttacking = false;
        OnRageBarEmptied();
    }
    public void AddAmmo(int toAdd)
    {
        Mathf.Clamp(currentAmmo += toAdd, 0, maxAmmo) ;
    }
    public void RemoveAmmo(int toRemove)
    {
        Mathf.Clamp(currentAmmo -= toRemove, 0, maxAmmo);
    }
    private void RotateBack()
    {
        if (directionFacing == Vector3.right)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }
        else if (directionFacing == Vector3.left)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
    }
    private void ShootProjectile()
    {
        Vector3 rotation = Vector3.zero;
        if (directionFacing == Vector3.right)
        {
            rotation = new Vector3(0, 0, -90f);
        }
        else if (directionFacing == Vector3.left)
        {
            rotation = new Vector3(0, 0, 90f);
        }
        else
        {
            return;
        }
        GameObject spawnedProjectile = Instantiate(rangeProjectile, bow.transform.position, Quaternion.Euler(rotation));
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
    public void OnPrimaryAtttackLanded()
    {
        characterManager.GetRageController().IncreaseRage(characterManager.GetRageController().primaryAttackPoints);
    }
}
