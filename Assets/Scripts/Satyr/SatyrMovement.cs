using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrMovement : MonoBehaviour, IMovement
{
    [SerializeField] float lateralMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityInAir;
    [SerializeField] float gravityOnWall;
    float val = 0;

    private SatyrFighter fighter;
    private Animator anim;
    private CharacterController characterController;
    private ICharacterManager characterManager;
    private bool isInContactWithHoppableWall;
    private bool canWallHop;

    private void Awake()
    {
        characterManager = GetComponent<ICharacterManager>();
        fighter = GetComponent<SatyrFighter>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }
    public void MoveHorizontally(float horiAxis)
    {
        if (fighter.isAttacking) return;

        float gravity = isInContactWithHoppableWall ? gravityOnWall : gravityInAir;

        val -= gravity * Time.deltaTime;

        characterController.Move(new Vector3(lateralMovementSpeed * horiAxis, val*3, 0) * Time.deltaTime);
      
        
        HandleRotation(horiAxis) ;
        HandleAnimation();

    }
    public void Jump()
    {
        if (fighter.isAttacking) return;
        if (characterController.isGrounded || (isInContactWithHoppableWall && canWallHop))
        {
            print("jump() called");

            anim.SetTrigger("Jump");
            val = jumpForce;
            canWallHop = false;

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            print("canWallHop = true");
            canWallHop = true;
            isInContactWithHoppableWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            print("canWallHop = false");
            //canWallHop = false;
            isInContactWithHoppableWall = false;
        }
    }


}
