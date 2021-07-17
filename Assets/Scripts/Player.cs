using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private Vector3 velocity; 
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public float mouseX;
    public float mouseY;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        if(controller == null)
        {
            Debug.Log(controller + "Character Controller is Null"); 
        }
    }

    void Update()
    {
        CharacterMovement(); 

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Apply rotation to player rotation y = look left and right 
        // Apply mouse Y to Camera x value 
        // transform.eulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z); 
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.rotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up); 

    }

    private void CharacterMovement()
    {

        if (controller.isGrounded == true)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            direction = new Vector3(horizontal, 0, vertical);
            velocity = direction * playerSpeed;

            // Changes the height position of the player..
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                velocity.y = jumpHeight;
            }
        }

        velocity.y += gravityValue * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
