using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{

    private CharacterController _controller;
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 5f;
    private bool isGrounded;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    private bool lerpCrouch = false;
    private float crouchTimer = 0f;
    private bool crouching = false;
   
    private bool sprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = _controller.isGrounded;

        if (lerpCrouch)
        {
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            else
                _controller.height = Mathf.Lerp(_controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }

        }
    }

    public void ProcessMove(Vector2 Input)
    {
       Vector3 moveDirection = Vector3.zero;
       moveDirection.x = Input.x;
       moveDirection.z = Input.y;
       _controller.Move(transform.TransformDirection(moveDirection) * speed *Time.deltaTime);
       
       playerVelocity.y += gravity * Time.deltaTime;
       if (isGrounded && playerVelocity.y < 0)
       {
           playerVelocity.y = -2f;
       }

       _controller.Move(playerVelocity * Time.deltaTime);
       print(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if (sprinting)
            speed = 8;
        else
            speed = 5;

    }
}
