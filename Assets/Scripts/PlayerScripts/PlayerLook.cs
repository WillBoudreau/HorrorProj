using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private PlayerInput playerInput;
    private InputAction lookAction;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float mouseSensitivity = 2f;
    
    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = playerMovement.rb;
        lookAction = playerInput.actions["Look"];
    }
    void Update()
    {
        Look();
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

        float rotationY = lookInput.x * mouseSensitivity * Time.deltaTime;
        // float rotationX = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Only rotate using Rigidbody to avoid double rotation
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationY, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
