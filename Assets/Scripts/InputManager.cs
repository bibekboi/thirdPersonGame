using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; 
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look; 

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor  = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();         
    }

    private void FixedUpdate()
    {
        //tell the player motor to move from value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

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
