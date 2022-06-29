using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] GameObject rangeProjectile;
    [SerializeField] GameObject bow;
    [SerializeField] private float projectileSpeed;
    private Animator anim;
    public bool isAttacking;

    public Vector3 directionFacing;
    private void Awake()
    {
        directionFacing = Vector3.right;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RangeAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        anim.SetTrigger("Melee");
    }

    private void RangeAttack()
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
        anim.SetTrigger("RangeAttack");
        print("Range attack");
    }
    public void EndRangeAttack()
    {
        print("EndRangeAttack() called");
        Invoke("RotateBack", 0.5f);
        isAttacking = false;
        ShootProjectile();
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
}
