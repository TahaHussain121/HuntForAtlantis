using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float lateralMovementSpeed;
    //[SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;

    private MinotaurFighter fighter;
    private Animator anim;
    private CharacterController characterController;
    private bool canWallHop;

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
        characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, -gravity, 0) * Time.deltaTime);
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
            characterController.Move(Vector3.up * (jumpForce - gravity) * Time.deltaTime);
        }
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.transform.tag == "Wall")
    //    {
    //        print("canWallHop = true");
    //        canWallHop = true;
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            print("canWallHop = true");
            canWallHop = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            print("canWallHop = false");
            canWallHop = false;
        }
    }

}