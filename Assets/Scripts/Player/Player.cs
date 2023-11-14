using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.SceneManagement; 
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

    [Header("Animation")]
    [SerializeField]
    private Animator anim;
    public bool isIdle;
    public bool isWalk;

    [SerializeField]
    private PowerBox power1;
    [SerializeField]
    private PowerBox power2;
    [SerializeField]
    private PowerBox power3;

    public GameObject ammoSupply;

    public bool ammoPickUp;

    private void Start()
    {
        ammoPickUp = false; 

        // lock cursor when game starts v
        Cursor.lockState = CursorLockMode.Locked;

        controller = gameObject.GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.Log(controller + "Character Controller is Null");
        }

        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError(mainCam + "Main Camera is Null");
        }

        if (anim != null)
        {
            anim = gameObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        CharacterMovement();
        CameraControl();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (power1.p1Restored == true && power2.p2Restored == true && power3.p3Restored == true)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
            // return to main menu 
            // end game 
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoPickUp == true)
        {
            ammoSupply.GetComponent<GunAmmo>().Ammo += 10;
            ammoSupply.GetComponent<GunAmmo>().ammoText.text = ammoSupply.ToString();
            if(ammoPickUp == true)
            {
                ammoPickUp = false; 
            }
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

        if (controller.isGrounded == true && horizontal > 0 || vertical > 0)
        {
            isIdle = false;
            if (isIdle == false)
            {
                isWalk = true;
                anim.SetBool("isIdle", false);
                anim.SetBool("Walk", true);
            }
        }
        if (controller.isGrounded == true && horizontal < 1 || vertical < 1)
        {
            isWalk = false;
            isIdle = true;
            if (isIdle == true)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("isIdle", true);
            }
        }


        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            yVelocity = jumpHeight;
        }

        yVelocity -= jumpHeight * Time.deltaTime;

        velocity.y = yVelocity; // set velocity 

        if (controller.enabled == true)
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Cube")
        {
            ammoPickUp = true;
           // Debug.Log("Cube");
            Destroy(hit.gameObject);
        }
    } 
}