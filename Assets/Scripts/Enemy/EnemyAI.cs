using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public enum EnemyState // FSM 
    {
        Idle, 
        Attack, 
        Chase 
    }

    [SerializeField]
    private EnemyState enemyState = EnemyState.Chase;

    // reference to Character Controller 
    private CharacterController controller;
    private Transform target; // player target 
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float gravity = 20; 
    private Vector3 velocity;

    private Health playerHealth;

    [SerializeField]
    private float attackDelay = 1.5f; 
    private float nextAttack = -1;

    [SerializeField]
    private Animator animator; 

    private void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if(controller == null)
        {
            Debug.LogError("Character Controller is Null"); 
        }

        target = GameObject.Find("Player").transform; 
        if(target == null)
        {
            Debug.LogError("Player is Null"); 
        }
        playerHealth = target.GetComponent<Health>(); 
        if(playerHealth == null || target == null)
        {
            Debug.LogError("Player Components are Null"); 
        }

        enemyState = EnemyState.Idle;
    }

    private void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Attack:
                Attack(); 
                break;
            case EnemyState.Chase:
                EnemyMovement();
                break;
            default:
                break; 
        }
    }

    private void Chase()
    {
        if (target != null)
        {

            animator.SetBool("Idle", false);
            animator.SetTrigger("Walking");
            Vector3.MoveTowards(transform.position, target.transform.position, nextAttack);
        }
    }

    private void Attack()
    {
        // cooldown system 
        if (Time.time > nextAttack && controller.isGrounded)
        {
            // damage player 
            if (playerHealth != null)
            {
                playerHealth.Damage(1);
            }
            nextAttack = Time.time + attackDelay;
        }
    }

    public void StartAttack()
    {
        animator.SetTrigger("Walking");
        enemyState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        enemyState = EnemyState.Chase;
    }

    public void Idle()
    {
        animator.SetBool("Idle", true); 
        enemyState = EnemyState.Idle; 
    }

    private void EnemyMovement()
    {
        // check if grounded 
        if (controller.isGrounded == true && gameObject != null)
        {
            animator.SetBool("Idle", false);
            enemyState = EnemyState.Chase;
            Chase(); 
            // calculate direction = destination (target) - start (self)
            if(target != null)
            {
                Vector3 direction = target.position - transform.position;
                // direction.y = 0;
                // direction.Normalize();
                // rotate towards the player 
                transform.LookAt(target);
                transform.localRotation = Quaternion.LookRotation(direction);
                // calculate velocity = direction * speed 
                velocity = direction * speed;

                velocity = transform.TransformDirection(velocity);

                // subtract gravity from velocity direction y 
                velocity.y -= gravity;

                // move to velovity 
                controller.Move(velocity * Time.deltaTime);
            }       
        }
    }
}