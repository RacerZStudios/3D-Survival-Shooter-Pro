using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // reference to Character Controller 
    private CharacterController controller;
    private Transform target; // player target 
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float gravity = 20; 
    private Vector3 velocity; 

    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
        if(controller == null)
        {
            Debug.LogError("Character Controller is Null"); 
        }

        target = GameObject.Find("Player").transform; 
        if(target == null)
        {
            Debug.LogError("Player is Null"); 
        }
    }

    private void Update()
    {
        // check if grounded 
        if(controller.isGrounded == true)
        {
            // calculate direction = destination (target) - start (self)
            Vector3 direction = target.position - transform.position;
            direction.y = 0; 
            direction.Normalize();
            // rotate towards the player 
            transform.localRotation = Quaternion.LookRotation(direction);
            // calculate velocity = direction * speed 
            velocity = direction * speed; 
        }

        velocity = transform.TransformDirection(velocity);

        // subtract gravity from velocity direction y 
        velocity.y -= gravity;

        // move to velovity 
        controller.Move(velocity * Time.deltaTime); 
    }
}
