using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;

    private bool isGrounded;
    private bool isCrouching;
    private bool isLerpCrouch;
    private bool isSprinting;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public float crouchTimer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        // For Crouching
        if(isLerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            
            if(isCrouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p );
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if(p > 1)
            {
                isLerpCrouch = false;
                crouchTimer = 0f; 
            }
        }
    }

    //Receive inputs from input manager and apply to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero; 
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0) 
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y); 
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
        isCrouching = !isCrouching;
        crouchTimer = 0f;
        isLerpCrouch = true;
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }



    /*
    public void Sprint()
    {
        isSprinting = !isSprinting;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = 8f;
            Debug.Log("Sprinting!");
        }
        else
        {
            isSprinting = !isSprinting;
            speed = 5f;
            Debug.Log("Not Sprinting"); 
        }
    }
    */

    //Backup Code

   /*
    public void Sprint()
    {
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }
   */



}
