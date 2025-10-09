using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    [Header("Slot References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Dropdown interactionDropdown;
    [SerializeField] private List<string> interactionOptions = new List<string>();

    void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
        if (interactionDropdown != null)
        {
            interactionDropdown.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sets the item icon for the inventory slot.
    /// </summary>
    public void SetItemIcon(Color color)
    {
        itemIcon.color = color;
    }
    /// <summary>
    /// When the interaction button is clicked, display the dropdown menu.
    /// </summary>
    public void OnInteractionButtonClicked()
    {
        if (interactionDropdown.gameObject.activeSelf)
        {
            interactionDropdown.gameObject.SetActive(false);
        }
        else
        {
            interactionDropdown.gameObject.SetActive(true);
        }
    }
    /// <summary>
    ///  Populates the dropdown with interaction options.
    /// </summary>
    public void PopulateDropdown(string[] options)
    {
        interactionDropdown.ClearOptions();
        interactionDropdown.AddOptions(new List<string>(options));
    }
    /// <summary>
    /// Sets the icon based on what the player has in their inventory.
    /// </summary>
    /// <param name="item"></param>
    public void UpdateSlot(GameObject item)
    {
        if (item != null)
        {
            InteractableOBJ interactable = item.GetComponent<InteractableOBJ>();
            if (interactable.pickupType == InteractableOBJ.PickupType.Food)
            {
                SetItemIcon(Color.orange);
                PopulateInteractionOptions(item);
            }
            else if (interactable.pickupType == InteractableOBJ.PickupType.Health)
            {
                SetItemIcon(Color.red);
                PopulateInteractionOptions(item);
            }
            else if (interactable.pickupType == InteractableOBJ.PickupType.Key)
            {
                SetItemIcon(Color.yellow);
                PopulateInteractionOptions(item);
            }
            else
            {
                SetItemIcon(Color.white);
                PopulateInteractionOptions(item);
            }
        }
        else
        {
            SetItemIcon(Color.white);
            PopulateInteractionOptions(null);
        }
    }
    /// <summary>
    /// Populates the interaction options based on the item type.
    /// </summary>
    public void PopulateInteractionOptions(GameObject item)
    {
        interactionOptions.Clear();
        if (item != null)
        {
            InteractableOBJ interactable = item.GetComponent<InteractableOBJ>();
            switch (interactable.interactionType)
            {
                case InteractableOBJ.InteractionType.Pickup:
                    interactionOptions.Add("-----");
                    interactionOptions.Add("Use");
                    interactionOptions.Add("Drop");
                    break;
                case InteractableOBJ.InteractionType.Examine:
                    interactionOptions.Add("-----");
                    interactionOptions.Add("Examine");
                    interactionOptions.Add("Drop");
                    break;
                case InteractableOBJ.InteractionType.Activate:
                    interactionOptions.Add("-----");
                    interactionOptions.Add("Activate");
                    interactionOptions.Add("Drop");
                    break;
                default:
                    interactionOptions.Add("No actions available");
                    break;
            }
        }
        else
        {
            interactionOptions.Add("Empty Slot");
        }
        PopulateDropdown(interactionOptions.ToArray());
    }
    /// <summary>
    /// Use the selected interaction from the dropdown.
    /// </summary>
    public void UseItem()
  {
        int selectedIndex = interactionDropdown.value;
        if (selectedIndex <= 0 || selectedIndex >= interactionOptions.Count)
        {
            Debug.Log("No valid action selected.");
            return;
        }

        string selectedAction = interactionOptions[selectedIndex];
        Debug.Log("Selected Action: " + selectedAction);

        // Implement action logic here
        switch (selectedAction)
        {
            case "Use":
                Debug.Log("Using item...");
                playerInventory.RemoveItem(playerInventory.inventoryItems[selectedIndex - 1]);
                UpdateSlot(null);
                break;
            case "Examine":
                Debug.Log("Examining item...");
                // Add logic for examining the item
                break;
            case "Activate":
                Debug.Log("Activating item...");
                // Add logic for activating the item
                break;
            case "Drop":
                Debug.Log("Dropping item...");
                // Add logic for dropping the item
                break;
            default:
                Debug.Log("Action not recognized.");
                break;
        }

        // Hide dropdown after action
        interactionDropdown.gameObject.SetActive(false);
  }
}
