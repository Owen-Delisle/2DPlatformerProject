using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] MainPlayerScript mainPlayerScript;

    // Player Forces
    [SerializeField] public float playerSpeed;
    [SerializeField] public float jumpForce;

    private int groundMask;

    // Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";

    // Start is called before the first frame update
    void Start()
    {
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if player is on the ground
        UpdateIsGrounded();

        // Plays correct grounded animation
        UpdateGroundedAnimations();

        // Initializes Player jump vector and animation
        PlayerJump();

        // Initializes Player fall animation
        PlayerFall();

        // Assign the new velocity to the rigidbody
        PlayerVelocity();
    }

    // Check if player is on the ground
    private void UpdateIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundMask);

        if (hit.collider != null)
        {
            mainPlayerScript.isGrounded = true;
        }
        else
        {
            mainPlayerScript.isGrounded = false;
        }
    }

    // Plays correct grounded animation
    private void UpdateGroundedAnimations()
    {
        if (mainPlayerScript.isGrounded)
        {
            if (mainPlayerScript.xAxis != 0)
            {
                mainPlayerScript.ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                mainPlayerScript.ChangeAnimationState(PLAYER_IDLE);
            }
        }
    }

    // Initializes Player jump vector and animation
    private void PlayerJump()
    {
        if (mainPlayerScript.isJumpPressed && mainPlayerScript.isGrounded)
        {
            mainPlayerScript.rigidBody.AddForce(new Vector2(0, jumpForce));
            mainPlayerScript.isJumpPressed = false;
            mainPlayerScript.ChangeAnimationState(PLAYER_JUMP);
        }
    }

    // Initializes Player fall animation
    private void PlayerFall()
    {
        if (!mainPlayerScript.isGrounded && mainPlayerScript.rigidBody.velocity.y < 0)
        {
            mainPlayerScript.ChangeAnimationState(PLAYER_FALL);
        }
    }

    // Assign the new velocity to the rigidbody
    private void PlayerVelocity()
    {
        Vector2 vel = new Vector2(0, mainPlayerScript.rigidBody.velocity.y);
        if (mainPlayerScript.xAxis < 0)
        {
            vel.x = -playerSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (mainPlayerScript.xAxis > 0)
        {
            vel.x = playerSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;
        }
        mainPlayerScript.rigidBody.velocity = vel;
    }
}
