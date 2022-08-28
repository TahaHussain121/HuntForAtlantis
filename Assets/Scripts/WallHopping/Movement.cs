using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float lateralMovementSpeed;
    //[SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityInAir;
    [SerializeField] float gravityOnWall;

    private MinotaurFighter fighter;
    private Animator anim;
    private CharacterController characterController;
    private bool canWallHop;
    private bool isInContactWithWall;

    private void Awake()
    {
        fighter = GetComponent<MinotaurFighter>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        //HandleAnimation();
    }
    public void MoveHorizontally(float horiAxis)
    {
        //if (fighter.isAttacking) return;
        if (isInContactWithWall)
        {
            characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, -gravityOnWall, 0) * Time.deltaTime);
        }
        else characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, -gravityInAir, 0) * Time.deltaTime);
        //HandleRotation(horiAxis);
        //HandleAnimation();

    }
    public void Jump()
    {
        //if (fighter.isAttacking) return;
        if (characterController.isGrounded || canWallHop)
        {
            //print("jump() called");
            canWallHop = false;
            characterController.Move(Vector3.up * (jumpForce - gravityInAir) * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            print("canWallHop = true");
            canWallHop = true;
            isInContactWithWall = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            print("canWallHop = false");
            canWallHop = false;
            isInContactWithWall = false;
        }
    }

}