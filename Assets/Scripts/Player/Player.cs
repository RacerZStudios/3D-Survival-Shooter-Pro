using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    [Header("Controller Settings")]
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float jumpHeight = 10.0f;

    private Vector3 direction;
    private Vector3 velocity; 

    private Camera mainCam;

    [Header("Camera Settings")]
    [SerializeField]
    private float mainCamSensityvity = 1.0f;

    public float mouseX;
    public float mouseY;
    private float yVelocity; 

    private void Start()
    {
        // lock cursor when game starts v
        Cursor.lockState = CursorLockMode.Locked; 

        controller = gameObject.GetComponent<CharacterController>();
        if(controller == null)
        {
            Debug.Log(controller + "Character Controller is Null"); 
        }

        mainCam = Camera.main;
        if(mainCam == null)
        {
            Debug.LogError(mainCam + "Main Camera is Null"); 
        }
    }

    void Update()
    {        
        CharacterMovement();
        CameraControl(); 

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    private void CameraControl()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Apply rotation to player rotation y = look left and right 
        // Apply mouse Y to Camera x value 
        // transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z); 
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * mainCamSensityvity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        // Look up and down
        Vector3 currentCameraRotation = mainCam.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * mainCamSensityvity;
        // Clamp camera rotation 
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0, 15); 
        // mainCam.gameObject.transform.localEulerAngles = currentCameraRotation; 
        mainCam.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    private void CharacterMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical);
        velocity = direction * playerSpeed;

        velocity = transform.TransformDirection(velocity); // velocity turned into local space to world space 

        if (controller.isGrounded == true)
        {
            // Changes the height position of the player..
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                yVelocity = jumpHeight;
            }
        }

        yVelocity -= jumpHeight * Time.deltaTime; 

        velocity.y = yVelocity; // set velocity 

        controller.Move(velocity * Time.deltaTime);
    }
}