using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        isGrounded = controller.isGrounded;
    }

    void handleAnimation()
    {
        // Check for walking
        // not working atm
        if (Mathf.Abs(playerVelocity.x) > 0 || Mathf.Abs(playerVelocity.z) > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Check for jumping
        if (!isGrounded && playerVelocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    // Method to handle player movement input
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        // Set the x and z components of the move direction based on the player's input
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Move the character controller in the direction calculated above, accounting for speed and time per frame
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Apply gravity to the player's vertical velocity over time
        playerVelocity.y += gravity * Time.deltaTime;

        // This prevents the player from slowly sinking into the ground due to small negative y-velocity values
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Method to handle jumping input
    public void Jump()
    {
        if (isGrounded)
        {
            // Set the vertical velocity to a value that will propel the player upwards based on jump height and gravity
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}