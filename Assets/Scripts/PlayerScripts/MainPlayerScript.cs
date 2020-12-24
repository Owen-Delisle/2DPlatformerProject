using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class MainPlayerScript : MonoBehaviour
{
    // Player Child Script References
    [SerializeField] PlayerInput playerInputScript;
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] PlayerCollision playerCollisionScript;

    // Object Components
    public Rigidbody2D rigidBody;
    private Animator animator;
    public SpriteRenderer spriteRenderer;

    // Movement Vars
    public float xAxis;
    public bool isJumpPressed;

    // Grounded Vars
    public bool isGrounded = false;

    // Anim Vars
    private string currentAnimaton;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}