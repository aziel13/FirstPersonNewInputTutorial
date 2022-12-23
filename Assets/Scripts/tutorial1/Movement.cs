using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float speed = 11f;
    [SerializeField] private float gravity = -30;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 3.5f;
    
    private bool jump;
    
    private bool isGrounded;
    
    private Vector3 verticalVelocity = Vector3.zero;    
    
    
    private Vector2 horizontalInput;


    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }
        
        

        Vector3 horizontalVelocity = transform.right * horizontalInput.x + transform.forward * horizontalInput.y ;
        horizontalVelocity *= speed;

        _characterController.Move(horizontalVelocity * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }

            jump = false;

        }

         _characterController.Move(verticalVelocity * Time.deltaTime);
 
        
        
        

    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
        print(horizontalInput);
    }

    public void OnJumpPressed()
    {
        jump = true;
        
    }

}
