using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private float gravity; //use the gravity of the earth: -9.81

    private CharacterController characterController; //Storing the CharacterController Component

    private Vector3 motion; //Vector3 we use to move the CharacterController

    [SerializeField] 
    private float jumpForce;

    [SerializeField]
    private Vector3 jump;


    /// <summary>
    /// Before we can go through with Update, we need to attach the script to a GameObject wtih a 
    /// CharacterController-Component to be able to even access a CharacterController and store it in a variable
    /// </summary>
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
    }


    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift)) //Running with Shift
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl)) //Sneaking/Crouching with Strg/Ctrl
        {
            moveSpeed = sneakSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        /// <summary>
        /// If the spacebar is pressed and the character is not already jumping
        /// </summary>
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            jump = new Vector3(0f, jumpForce, 0f);
        }
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // We need to get the input values of the axis every frame in order tu move in the direction of our input
        // x is the horizontalInput value which will move the characterController right or left
        // y is set to the gravity value which will pull the characterController down 
        // z is set to the verticalInput value which will move the characterController forwards or backwards
        motion = new Vector3(horizontalInput, gravity, verticalInput);
       /// motion = transform.right * horizontalInput + transform.forward * verticalInput - transform.up * gravity + jumpForce;
      


        // We apply the Vector for movement (named Motion) to the .Move() method of the characterController 
        // In order to stay framerate independent we multiply the vector by Time.deltaTime and also
        // by the moveSpeed in order to maintain control of speed
        characterController.Move(motion: motion * moveSpeed * Time.deltaTime);
    }
}

