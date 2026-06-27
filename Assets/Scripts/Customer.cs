using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private const float MOVEMENT_SPEED = 5f;
    private const float ROTATION_SPEED = 10f;
    
    [SerializeField] private CustomerVisual customerVisual;
    
    private Animator animator;
    private Vector3 targetPosition;
    private bool isWalking = false;
    private bool isLeaving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (customerVisual == null)
        {
            customerVisual = GetComponent<CustomerVisual>();
        }
    }

    private void Update()
    {
        // Check if we are not at our target position yet
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Calculate direction and move
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MOVEMENT_SPEED * Time.deltaTime);
            
            // Rotate smoothly towards the walking direction
            transform.forward = Vector3.Slerp(transform.forward, moveDir, ROTATION_SPEED * Time.deltaTime);
            
            // Trigger the run animation once
            if (!isWalking)
            {
                isWalking = true;
                animator.SetTrigger("run");
            }
        }
        else
        {
            // We have reached the target position
            if (isWalking)
            {
                isWalking = false;
                animator.SetTrigger("idle");
                
                // Snap exactly to position to be perfectly aligned
                transform.position = targetPosition;
            }

            if (isLeaving)
            {
                Destroy(gameObject);
            }
        }
    }

    public void InitializePosition(Vector3 startPosition)
    {
        transform.position = startPosition;
        targetPosition = startPosition;
    }

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }

    public void Leave(bool isHappy)
    {
        isLeaving = true;
        customerVisual.SetReaction(isHappy);
        
        targetPosition = transform.position - transform.right * 15f;
    }
}