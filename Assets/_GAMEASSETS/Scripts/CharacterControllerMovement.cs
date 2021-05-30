using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerMovement : MonoBehaviour
{
 
    //Variables
    // Serializing values to assign them in the Unity Inspector

    private float moveSpeed; //the speed the character is currently moving with aka initial speed

    [SerializeField]
    private float walkSpeed; //the medium speed, the character uses when walking casually

    [SerializeField]
    private float runSpeed; //the fastest speed, the character uses when running

    [SerializeField]
    private float sneakSpeed; //the slowest speed, the character uses when sneaking

    //GRAVITY
    [SerializeField] private float gravity; //use the gravity of the earth: -9.81
    
    private CharacterController characterController; //Storing the CharacterController Component

    private Vector3 motion; //Vector3 we use to move the CharacterController

    [SerializeField] 
    private float jumpForce; //The force which should be applied upwards when jumping

    private Vector3 jump; //Vector 3 which describes the jump vector

    [SerializeField] 
    private float jumpSmoother; //How smooth the jump speed should decrease

    
    //NEW INPUT SYSTEM:
    private Vector2 rawInputAxis; //the input Movement from the Input System (declared in "OnMovement()")
    private bool isSneaking; //if the player is sneaking
    private bool isSprinting; //if the player is sprinting
    private bool isGliding; //if the player is gliding
    private bool isFalling; //if the player is Falling
    
    /// <summary>
    /// Before we can go through with Update, we need to attach the script to a GameObject wtih a 
    /// CharacterController-Component to be able to even access a CharacterController and store it in a variable
    /// </summary>
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (jump.y > 0) //If the jumpvector is not 0 it should decrease by the jumpsmooth-amount
        {
            jump.y -= jumpSmoother;
        }
        else
        {
            jump.y = 0f;
        }
    }

    private void Update()
    {
         if (isSprinting) //Running with Shift
        {
            moveSpeed = runSpeed;
        }
        else if (isSneaking) //Sneaking/Crouching with Strg/Ctrl
        {
            moveSpeed = sneakSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        
        float horizontalInput = rawInputAxis.x;
        float verticalInput = rawInputAxis.y;

        // We need to get the input values of the axis every frame in order tu move in the direction of our input
        // x is the horizontalInput value which will move the characterController right or left
        // y is set to the gravity value which will pull the characterController down 
        // z is set to the verticalInput value which will move the characterController forwards or backwards
        //motion = new Vector3(horizontalInput, gravity, verticalInput);
        motion = (transform.right * horizontalInput) + (transform.forward * verticalInput) - (transform.up * gravity) + jump;
        

        // We apply the Vector for movement (named "motion") to the .Move() method of the characterController 
        // In order to stay framerate independent we multiply the vector by Time.deltaTime and also
        // by the moveSpeed in order to maintain control of speed
        characterController.Move(motion: motion * moveSpeed * Time.deltaTime);
        Debug.Log(message: "Height: " + transform.position.y);


    }

    
    //The input Actions that get called from the new input manager
    #region InputActions
    
    //Gets called when the Player uses Movement-Keys/Buttons
    public void OnMovement(InputAction.CallbackContext value)
    {
        rawInputAxis = value.ReadValue<Vector2>();
    }

    //Gets called when the Player uses the Jump-Key/Button
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (characterController.isGrounded)
            {
                jump = new Vector3(0f, jumpForce, 0f);
            }
        }

        if (value.performed)
        {
            isGliding = true;
        }
        else
        {
            isGliding = false;
        }
    }

    //Gets called when the Player uses the Sprint-Key/Button
    public void OnSprint(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    //Gets called when the Player uses the Sneak-Key/Button
    public void OnSneak(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }
    }
    #endregion
}

