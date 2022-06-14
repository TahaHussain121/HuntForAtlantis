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
    [SerializeField]
    private Transform _modeltransform;

    public void Start()
    {
        _originalheight = _controller.height;
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
                _controller = Gamemanager.SwitchCharacter();
            }

        }
        else
        {

            Debug.Log("Not on ground");
            if (Input.GetKeyDown(KeyCode.Space) && _candoublejump == true)
            {
                DoubleJump();
            }
            Gravity();
        }


           

        _controller.Move(VelocityCalculator(_yvelocity) * Time.deltaTime);

    }
    public void Jump()
    {

        _yvelocity = _jumpheight;
        _candoublejump = true;

    }
    public void DoubleJump()
    {

        _yvelocity += _jumpheight;
        _candoublejump = false;

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

}

