using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform Camera;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // reset velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // moving vector
        Vector3 move = Camera.transform.right * x + Camera.transform.forward * z;

        // move the player
        controller.Move(move * speed * Time.deltaTime);

        // player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // falling down
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
            ////////////////
        }
        else
        {
            isMoving = false;
            /////////////////
        }

        lastPosition = gameObject.transform.position;
    }
}
