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

        // if (IsGrounded() && jumpAction.triggered)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        // }
    }

    void Move()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        Vector3 newVelocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = newVelocity;
    }
    void Look()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        Vector3 lookDirection = new Vector3(lookInput.x, 0, lookInput.y);
        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }

}
