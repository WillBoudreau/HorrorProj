using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private UIManager uiManager;
    private InputAction lookAction;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float maxLookAngleX = 80f; // Maximum vertical look angle
    [SerializeField] private float minLookAngleX = -80f; // Minimum vertical look angle
    [SerializeField] private Transform cameraTransform; // Assign your camera here
    private float xRotation = 0f; // Tracks vertical rotation

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        rb = playerMovement.rb;
        lookAction = playerInput.actions["Look"];
        uiManager = FindObjectOfType<UIManager>();
 
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }
    void Update()
    {
        if(uiManager.mainMenu.activeSelf || uiManager.pauseMenu.activeSelf || uiManager.gameOverScreen.activeSelf || uiManager.settingsMenu.activeSelf || uiManager.inventoryUI.activeSelf|| uiManager.homeMainUI.activeSelf)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            return;
        }
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

        float mouseX = lookInput.x * Settings.playerSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * Settings.playerSensitivity * Time.deltaTime;

        if (Settings.invertYAxis == true)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));

        xRotation = Mathf.Clamp(xRotation, minLookAngleX, maxLookAngleX);

        cameraTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
    }
}