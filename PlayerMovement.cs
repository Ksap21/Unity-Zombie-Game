using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    Vector3 playerVelocity;
    private bool isGrounded;
    private bool isMoving;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float gravity = -9.8f * 2;
    public float jumpHeight = 3f;
    public float speed = 12f;
    public Animator animator;

    private Vector3 lastPosition =new Vector3(0f, 0f, 0f);
 
  
 
    void Start()
    {
        controller = GetComponent<CharacterController>();
     
        
    }

    void Update()
    {
    
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 move = transform.right * x + transform.forward * z;

        // Move character
        controller.Move(move * speed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //animator.SetBool("Idle", false);
            //animator.SetTrigger("Jump");
        }
        //else
        //{
        //    animator.SetBool("Idle", true);
        //      animator.ResetTrigger("Jump");
        //}

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move player vertically
        controller.Move(playerVelocity * Time.deltaTime);

       // Determine if player is moving
        isMoving = move.magnitude > 0 && isGrounded;

        // Update last position
        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
            //animator.SetBool("IsMoving", true);
            //animator.SetBool("Idle", false);
        }
        else
        {
            isMoving = false;
            //animator.SetBool("Idle", true);
            //animator.SetBool("IsMoving", false);

        }
        //if (isMoving == true)
        //{
        //    animator.SetBool("Idle", false);
        //    animator.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("Idle", true);
        //    animator.SetBool("IsMoving", false);
        //}


        lastPosition = gameObject.transform.position;
       
}   
}
