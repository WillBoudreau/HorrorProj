using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [Header("Movement Components")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        // jumpAction = playerInput.actions["Jump"];
    }
    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        Vector3 moveDirection = new Vector3(move.x, 0, move.z);
        moveDirection = transform.TransformDirection(moveDirection);
        Vector3 newVelocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = newVelocity;
    }
    void Look()
    {
        // Lock the cursor to the center of the screen and hide it
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float mouseSensitivity = 2f; // You can expose this as a [SerializeField] if needed

        // Rotate the player horizontally (y-axis) based on mouse X movement
        float rotationY = lookInput.x * mouseSensitivity;
        transform.Rotate(0f, rotationY, 0f, Space.World);

        // Rotate the rigidbody in the direction the player is facing
        rb.MoveRotation(Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
    }

}
