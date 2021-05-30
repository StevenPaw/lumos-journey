using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
        
    [SerializeField] private Transform targetTransform;
    private float pitch;
    private float yaw;
    [Header("Smooth Motion")]
    [SerializeField] private float smoothHorizontal;
    [SerializeField] private float smoothVertical;
    private float hVelocity = 0.0f;
    private float vVelocity = 0.0f;
    private Vector2 rawLookingAxis;

    private CharacterControllerMovement characterControllerMovement;

    private void Start()
    {
        characterControllerMovement = GetComponent<CharacterControllerMovement>();
    }

    private void Update()
    {
        float mouseX = rawLookingAxis.x * mouseSensitivity;
        float mouseY = rawLookingAxis.y * mouseSensitivity;

        pitch = Mathf.SmoothDamp(pitch, pitch - mouseY, ref vVelocity, smoothVertical); //smoothes pitch
        pitch = Mathf.Clamp(pitch, -45f, 45f); //clamps the vertical movement to not go upside down
            
        yaw = Mathf.SmoothDamp(yaw, yaw + mouseX, ref hVelocity, smoothHorizontal); //smoothes yaw
        
        targetTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f); //sets the vertical rotation of the target transform to new value
        
        transform.localRotation = Quaternion.Euler(0, yaw, 0f); //sets the horizontal rotation of the player GO to the new rotation
    }
    
    public void OnLooking(InputAction.CallbackContext value)
    {
        if (!characterControllerMovement.IsPaused)
        {
            rawLookingAxis = value.ReadValue<Vector2>(); //Reads the Values of the mouse or right joystick
        }
    }
    
    public void OnMoving(InputAction.CallbackContext value)
    {
        //TODO: It should be possible to rotate around the character as long as it doesnt move
        //transform.rotation = Quaternion.Euler(0, playerTransform.transform.rotation.eulerAngles.y, 0);
        //playerTransform.localEulerAngles = new Vector3(pitch, 0, 0);

        //playerTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        //transform.localRotation = Quaternion.Euler(0, yaw * 2, 0f);
    }
}
