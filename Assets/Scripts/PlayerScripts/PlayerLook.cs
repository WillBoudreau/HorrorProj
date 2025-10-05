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
    [SerializeField] private float maxLookAngleX = 80f; // Maximum vertical look angle
    [SerializeField] private float minLookAngleX = -80f; // Minimum vertical look angle
    [SerializeField] private Transform cameraTransform; // Assign your camera here
    private float xRotation = 0f; // Tracks vertical rotation

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = playerMovement.rb;
        lookAction = playerInput.actions["Look"];
 
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
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

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngleX, maxLookAngleX);

        cameraTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
    }
}