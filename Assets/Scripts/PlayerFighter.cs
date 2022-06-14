using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] GameObject rangeProjectile;
    private Animator anim;
    private void Awake()
    {
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
        Instantiate(rangeProjectile, transform.position, Quaternion.identity);
    }
}
