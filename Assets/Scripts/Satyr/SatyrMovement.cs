using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrMovement : MonoBehaviour
{
    [SerializeField] float lateralMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;

    private SatyrFighter fighter;
    private Animator anim;
    private CharacterController characterController;

    private void Awake()
    {
        fighter = GetComponent<SatyrFighter>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }
    public void MoveHorizontally(float horiAxis)
    {
        if (fighter.isAttacking) return;
        characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, -gravity, 0) * Time.deltaTime);
        HandleRotation(horiAxis) ;
        HandleAnimation();

    }
    public void Jump()
    {
        if (fighter.isAttacking) return;
        if (characterController.isGrounded)
        {
            //print("jump() called");
            characterController.Move(Vector3.up * (jumpForce - gravity) * Time.deltaTime);
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
