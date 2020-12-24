using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] PlayerController playerControlller;

    // Player Forces
    [SerializeField] public float playerSpeed;
    [SerializeField] public float jumpForce;

    // Ground Check Variables
    private int groundMask;
    [SerializeField] private float groundCheckDistance;

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

        // Initializes Player jump vector and animation
        PlayerJump();

        // Assign the new velocity to the rigidbody
        PlayerVelocity();
    }

    // Check if player is on the ground
    private void UpdateIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);

        if (hit.collider != null)
        {
            playerControlller.isGrounded = true;
        }
        else
        {
            playerControlller.isGrounded = false;
        }
    }

    // Initializes Player jump vector and animation
    private void PlayerJump()
    {
        if (playerControlller.isJumpPressed && playerControlller.isGrounded)
        {
            playerControlller.rigidBody.AddForce(new Vector2(0, jumpForce));
            playerControlller.isJumpPressed = false;
        }
    }

    // Assign the new velocity to the rigidbody
    private void PlayerVelocity()
    {
        Vector2 vel = new Vector2(0, playerControlller.rigidBody.velocity.y);
        if (playerControlller.xAxis < 0)
        {
            vel.x = -playerSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (playerControlller.xAxis > 0)
        {
            vel.x = playerSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;
        }
        playerControlller.rigidBody.velocity = vel;
    }
}
