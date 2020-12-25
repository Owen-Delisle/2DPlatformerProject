using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    // Reference to Parent Player Script
    [SerializeField] PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Checking for horizontal inputs
        playerController.xAxis = Input.GetAxisRaw("Horizontal");

        // Handle Space Key Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.isJumpPressed = true;
        }

        // Update Current Player Mouse Position
        playerController.mousePosition = GetMousePosition();

        // Handle Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            playerController.isMinePressed = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerController.isPlacePressed = true;
        }
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
