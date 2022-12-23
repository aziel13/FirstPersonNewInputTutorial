using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private MouseLook _mouseLook;
    private PlayerControls _playerControls;
    // private PlayerControls.GroundMovementActions _groundMovement;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        // _groundMovement = _playerControls.GroundMovement;

        // _groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        
        // _groundMovement.Jump.performed += _ => _movement.OnJumpPressed();

        // _groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        // _groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      _movement.ReceiveInput(horizontalInput);
      
      _mouseLook.ReceiveInput(mouseInput);
    }
}
