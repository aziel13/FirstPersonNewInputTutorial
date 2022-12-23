using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager2 : MonoBehaviour
{
    private PlayerControls _playerInput;
    public PlayerControls.OnFootActions onFoot;

    private playerMotor _motor;
    private PlayerLook _look;
    
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _playerInput = new PlayerControls();
        onFoot = _playerInput.OnFoot;

        _motor = GetComponent<playerMotor>();
        _look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => _motor.Jump();
        onFoot.Crouch.performed += ctx => _motor.Crouch();
        onFoot.Sprint.performed += ctx => _motor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
