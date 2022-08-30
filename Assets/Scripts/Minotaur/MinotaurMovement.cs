using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovement : MonoBehaviour
{
    [SerializeField] float lateralMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    float val = 0;

    private MinotaurFighter fighter;
    private Animator anim;
    private CharacterController characterController;

    private void Awake()
    {
        fighter = GetComponent<MinotaurFighter>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        HandleAnimation();
    }
    public void MoveHorizontally(float horiAxis)
    {
        if (fighter.isAttacking) return;

        val -= gravity * Time.deltaTime;
        characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, val * 3, 0) * Time.deltaTime);


        HandleRotation(horiAxis);
        HandleAnimation();
    }
    public void Jump()
    {
        if (fighter.isAttacking) return;
        if (characterController.isGrounded)
        {
            print("jump() called");

            anim.SetTrigger("Jump");
            val = jumpForce;

        }

    }
    private void HandleRotation(float axis)
    {
        if (axis > 0)
        {
            if (fighter.directionFacing != Vector3.right)
            {
                Quaternion toRotate = Quaternion.LookRotation(Vector3.right, Vector3.up);

                characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation,
                                            toRotate,
                                            Time.deltaTime * rotationSpeed);
                if (characterController.transform.rotation == toRotate)
                {
                    fighter.directionFacing = Vector3.right;
                }
            }
        }
        else if (axis < 0)
        {
            if (fighter.directionFacing != Vector3.left)
            {
                Quaternion toRotate = Quaternion.LookRotation(Vector3.left, Vector3.up);
                characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation,
                                            toRotate,
                                            Time.deltaTime * rotationSpeed);
                if (characterController.transform.rotation == toRotate)
                {
                    fighter.directionFacing = Vector3.left;
                }
            }
        }
    }
    private void HandleAnimation()
    {
        if (characterController.velocity.x != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }
}
