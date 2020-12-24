using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] MainPlayerScript mainPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for horizontal inputs
        mainPlayerScript.xAxis = Input.GetAxisRaw("Horizontal");

        // Handle Space Key Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainPlayerScript.isJumpPressed = true;
        }
    }
}
