
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 20f;
    [Header("Interaction References")]
    [SerializeField] private UIManager uiManager;
    public LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InteractableOBJ currentInteractable;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInventory playerInventory;
    private InputAction interactAction;
    private InputAction fireAction;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInventory = GetComponent<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();


        interactAction = playerInput.actions["OnInteract"];
        fireAction = playerInput.actions["Fire"];
    }
    void Update()
    {
        Interact();
        OnMouseDown();
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
    /// <summary>
    /// When the player clicks a family member, display their status.
    /// </summary>
    void OnMouseDown()
    {
        if (fireAction.triggered)
        {
            Debug.Log("Fire action triggered");
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                if (hit.collider.CompareTag("FamilyMember"))
                {
                    FamilyMemberBehaviour member = hit.collider.GetComponent<FamilyMemberBehaviour>();
                    if (member != null)
                    {
                        Debug.Log("Clicked on family member: " + member.memberName);
                        uiManager.UpdateFamilyMemberStatusUI(member);
                    }
                }
            }
        }
    }
        
    /// <summary>
    /// Check if the player is interacting with a specific family icon.
    /// </summary>
    public bool IsInteractingWithIcon(FamilyIcon familyIcon)
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            FamilyIcon hitIcon = hit.collider.GetComponent<FamilyIcon>();
            if (hitIcon != null && hitIcon == familyIcon)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Move the icon around with the mouse. So long as the player is holding down the fire button.
    /// </summary>
    public void DragIconWithMouse(FamilyIcon familyIcon)
    {
        if (fireAction.IsPressed())
        {
            Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                Vector3 newIconPosition = hit.point;
                familyIcon.transform.position = newIconPosition;
                Debug.Log("Dragging icon to position: " + newIconPosition);
            }
        }
    }

}