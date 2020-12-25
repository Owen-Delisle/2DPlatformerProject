using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Child Script References
    [SerializeField] UserInput inputScript;
    [SerializeField] Movement movementScript;
    [SerializeField] Collision collisionScript;
    [SerializeField] Mining miningSctipt;
    public TerrainController terrainController;

    // Object Components
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    // Movement Vars
    public float xAxis;
    public bool isJumpPressed;

    // Mining Vars
    public bool isMinePressed;
    public bool isPlacePressed;
    public Vector3 mousePosition;

    // Grounded Vars
    public bool isGrounded = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
