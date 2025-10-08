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
    private InputAction interactAction;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
        interactAction = playerInput.actions["OnInteract"];
    }
    void Update()
    {
        Interact();
    }
    /// <summary>
    /// Checks for interactable objects in front of the player.
    /// </summary>
    void InteractWithObj()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            currentInteractable = hit.collider.GetComponent<InteractableOBJ>();
            currentInteractable.Interact();
            CollectItem(currentInteractable);
            Debug.Log("Found interactable: " + currentInteractable.name);
        }
        else
        {
            OpenInventory();
        }
    }
    /// <summary>
    /// Handles player input for interactions.
    /// </summary>
    public void Interact()
    {
        if (currentInteractable == null && interactAction.triggered)
        {
            InteractWithObj();
        }
        else if (currentInteractable != null && interactAction.triggered)
        {
            InteractWithObj();
        }
    }
    ///<summary>
    /// Open the Inventory UI of the player.
    /// </summary>
    public void OpenInventory()
    {
        if (playerInventory != null)
        {
            playerInventory.ToggleInventory();
        }
    } 
    /// <summary>
    /// Picks up the specified item and adds it to the player's inventory.
    /// </summary>
    void CollectItem(InteractableOBJ currentInteractable)
    {
        if (currentInteractable != null && !currentInteractable.isCollected)
        {
            playerInventory.AddItem(currentInteractable.gameObject);
            currentInteractable.isCollected = true;
            currentInteractable.HideInteractionPrompt();
            currentInteractable.gameObject.SetActive(false);
        }
    }
}