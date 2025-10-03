using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InteractableOBJ currentInteractable;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInventory playerInventory;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
    }
    void Update()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionRange, Color.red);
    }
    /// <summary>
    /// Handles player interaction with objects, called from Input System
    /// </summary>
    /// <param name="context"></param>
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
            {
                InteractableOBJ interactable = hit.collider.GetComponent<InteractableOBJ>();
                if (interactable != null)
                {
                    currentInteractable = interactable;
                    currentInteractable.Interact();
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable.HideInteractionPrompt();
                    currentInteractable = null;
                }
            }
        }
    }
    /// <summary>
    /// Handles showing and hiding interaction prompts based on player's view
    /// </summary>
    public void Look(InputAction.CallbackContext context)
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            InteractableOBJ interactable = hit.collider.GetComponent<InteractableOBJ>();
            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.HideInteractionPrompt();
                    }
                    currentInteractable = interactable;
                    currentInteractable.ShowInteractionPrompt();
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.HideInteractionPrompt();
                currentInteractable = null;
            }
        }
    }
}