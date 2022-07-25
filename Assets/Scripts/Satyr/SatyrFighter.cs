using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrFighter : MonoBehaviour
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
    public bool isAttacking;
    public Vector3 directionFacing;
    private void Awake()
    {
        directionFacing = Vector3.right;
        anim = GetComponent<Animator>();
    }
    internal void PrimaryAttack()
    {
        RangeAttack();
    }
    internal void SpecialAttack()
    {
        if (isRageBarFull)
        {
            SpecialRangeAttack();
        }
        isRageBarFull = false;
    }
    private void SpecialRangeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("SpecialRangeAttack");
            //print("SpecialRangeAttack");
        }
    }
    private void RangeAttack()
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
            anim.SetTrigger("RangeAttack");
            //print("Range attack");
        }
    }
    public void AddAmmo(int toAdd)
    {
        Mathf.Clamp(currentAmmo += toAdd, 0, maxAmmo) ;
    }
    public void RemoveAmmo(int toRemove)
    {
        Mathf.Clamp(currentAmmo -= toRemove, 0, maxAmmo);
    }
    public void EndRangeAttack()
    {
        //print("EndRangeAttack() called");
        Invoke("RotateBack", 0.5f);
        ShootProjectile();
        isAttacking = false;
    }
    public void EndSpecialRangeAttack()
    {
        //print("EndSpecialRangeAttack() called");
        ArrowSpawner.SpawnArrows();
        isAttacking = false;
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
}
