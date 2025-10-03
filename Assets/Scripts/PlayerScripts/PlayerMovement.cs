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
    [SerializeField] private Camera playerCamera;
    public Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    private InputAction moveAction;

    private InputAction jumpAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = playerInput.actions["Move"];
        // jumpAction = playerInput.actions["Jump"];
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Get camera-relative directions
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Flatten so movement doesn’t tilt with camera up/down
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Combine with input
        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        // Apply velocity
        Vector3 moveVelocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
    }
}
