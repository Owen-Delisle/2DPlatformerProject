using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class PlayerController : MonoBehaviour
{
    // Object Components
    private Rigidbody2D rigidBody;
    public Animator animator;

    // Movement Vars
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    private int groundMask;
    private float xAxis;
    private bool isJumpPressed;

    // Grounded Vars
    public bool isGrounded = false;

    // Anim Vars
    private string currentAnimaton;

    //Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        // Checking for horizontal inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        // Handle Space Key Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Check if player is on the ground
        UpdateIsGrounded();

        // Plays correct grounded animation
        UpdateGroundedAnimations();

        // Initializes Player jump vector and animation
        PlayerJump();

        // Assign the new velocity to the rigidbody
        PlayerVelocity();

    }

    // Check if player is on the ground
    private void UpdateIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void UpdateGroundedAnimations()
    {
        if (isGrounded)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
    }

    private void PlayerJump()
    {
        if (isJumpPressed && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }
    }

    // Assign the new velocity to the rigidbody
    private void PlayerVelocity()
    {
        Vector2 vel = new Vector2(0, rigidBody.velocity.y);
        if (xAxis < 0)
        {
            vel.x = -playerSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = playerSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;
        }
        rigidBody.velocity = vel;
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}