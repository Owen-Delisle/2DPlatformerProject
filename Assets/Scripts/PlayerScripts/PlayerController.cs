using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class PlayerController : MonoBehaviour
{
    // Object Components
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

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

    // Animation States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";

    // Collider Tags
    const string waterCollider = "Water";
    const string squareMaceCollider = "SquareMace";

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // Plays correct grounded animation
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

    // Initializes Player jump vector and animation
    private void PlayerJump()
    {
        if (isJumpPressed && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);   
        }
    }

    // Initializes Player fall animation
    private void PlayerFall()
    {
        if (!isGrounded && rigidBody.velocity.y < 0)
        {
            ChangeAnimationState(PLAYER_FALL);
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

    // ================ Collision ===============

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case squareMaceCollider:
                StartCoroutine(PlayerHurt());
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case waterCollider:
                Debug.Log("Im Wet!");
                break;
        }
    }

    IEnumerator PlayerHurt()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1);
        spriteRenderer.color = Color.white;
    }
}