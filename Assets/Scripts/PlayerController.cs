using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isStrafeingHash;
    int isJumpingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isStrafeingHash = Animator.StringToHash("isStrafeing");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool moveForward = Input.GetKey("w");
        bool isStrafeing = animator.GetBool("isStrafeing");
        bool moveRight = Input.GetKey("d");
        bool moveLeft = Input.GetKey("a");
        bool isJumping = animator.GetBool("isJumping");
        bool jump = Input.GetKey("space");

        if (!isWalking && moveForward)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !moveForward)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isStrafeing && moveRight)
        {
            animator.SetBool(isStrafeingHash, true);
        }
        if (isStrafeing && !moveRight)
        {
            animator.SetBool(isStrafeingHash, false);
        }
        if (!isStrafeing && moveLeft)
        {
            animator.SetBool(isStrafeingHash, true);
        }
        if (isStrafeing && !moveLeft)
        {
            animator.SetBool(isStrafeingHash, false);
        }
        if (!isJumping && jump)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && !jump)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}
