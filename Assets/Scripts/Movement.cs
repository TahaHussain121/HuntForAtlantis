using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationMultiplier;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _jumpheight;
    [SerializeField]
    private float _yvelocity;
    [SerializeField]
    private bool _candoublejump = false;
    [SerializeField]
    private bool _iscrouching = false;
    [SerializeField]
    private float _originalheight;
    [SerializeField]
    private float _newheight;
    //[SerializeField]
    //private Transform _modeltransform;
    public Animator _anim;
    public PlayerFighter _fighter;



    public void Start()
    {
        _originalheight = _controller.height;
        _anim = _controller.GetComponent<Animator>();
        _fighter = _controller.GetComponent<PlayerFighter>();
    }
    public void Update()
    {
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                _iscrouching = true;
                Crouch(_iscrouching);
                Debug.Log("Crouching");

            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                _iscrouching = false;
                Debug.Log("up ");
                Crouch(_iscrouching);
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                _anim.SetBool("Running", false);

                _controller = Gamemanager.SwitchCharacter();
                _anim = _controller.GetComponent<Animator>();
                _fighter = _controller.GetComponent<PlayerFighter>();
            }
            HandleAnimation(true);

        }
        else
        {

            Debug.Log("Not on ground");
            if (Input.GetKeyDown(KeyCode.Space) && _candoublejump == true)
            {
                DoubleJump();
            }
            Gravity();
            HandleAnimation(true);
        }
        if (!_fighter.isAttacking)
        {
            _controller.Move(VelocityCalculator(_yvelocity) * Time.deltaTime);
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        float axis = Input.GetAxis("Horizontal");
        if (axis > 0)
        {
            if (_fighter.directionFacing != Vector3.right)
            {
                Quaternion toRotate = Quaternion.LookRotation(Vector3.right, Vector3.up);

                _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation,
                                            toRotate, 
                                            Time.deltaTime * _rotationMultiplier);
                if (_controller.transform.rotation == toRotate)
                {
                    _fighter.directionFacing = Vector3.right;
                }
            }
        }
        else if (axis < 0)
        {
            if (_fighter.directionFacing != Vector3.left)
            {
                Quaternion toRotate = Quaternion.LookRotation(Vector3.left, Vector3.up);
                _controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation,
                                            toRotate,
                                            Time.deltaTime * _rotationMultiplier);
                if (_controller.transform.rotation == toRotate)
                {
                    _fighter.directionFacing = Vector3.left;
                }
            }
        }
    }

    public void Jump()
    {

        _yvelocity = _jumpheight;
        _candoublejump = true;
        _anim.SetTrigger("Jump");

    }
    public void DoubleJump()
    {
        _yvelocity += _jumpheight;
        _candoublejump = false;
        _anim.SetTrigger("Jump");
    }
    public void Crouch(bool input)
    {

        float centre;
        if (input)
        {
            _newheight = (_originalheight * 0.5f);
            centre = 0.5f;
        }
        else
        {
            _newheight = _originalheight;
            centre = 0;

        }
        _controller.height = _newheight;
        _controller.center = new Vector3(_controller.center.x, centre, _controller.center.z);


    }
    public void Gravity()
    {
        _yvelocity -= _gravity;

    }

    public Vector3 VelocityCalculator(float updatevelocity)
    {

        float input = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(input, -0.1f, 0);
        Vector3 velocity = dir * _speed;
        velocity.y = updatevelocity;
        return velocity;
    }

    private void HandleAnimation(bool grounded)
    {
        if (grounded)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                _anim.SetBool("Running", true);
            }
            else
            {
                _anim.SetBool("Running", false);
            }
        }
    }

}

