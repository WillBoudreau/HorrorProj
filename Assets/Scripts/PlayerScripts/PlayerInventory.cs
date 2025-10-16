using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxItems = 10;
    private int currentItemCount = 0;
    public List<GameObject> inventoryItems = new List<GameObject>();
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField] private UIManager uiManager;
    [Header("Item bag")]
    [SerializeField] private List<InventorySlot> itemBagSlots = new List<InventorySlot>();
    [SerializeField] private List<GameObject> itemBagItems = new List<GameObject>();
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    public void AddItem(GameObject item)
    {
        if (currentItemCount < maxItems)
        {
            currentItemCount++;
            inventoryItems.Add(item);
            itemBagItems.Add(item);
            inventorySlots[currentItemCount - 1].UpdateSlot(item);
            itemBagSlots[currentItemCount - 1].UpdateSlot(item);
            Debug.Log("Item added. Current item count: " + currentItemCount);
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
    /// <summary>
    /// Removes an item from the inventory.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(GameObject item)
    {
        if (inventoryItems.Contains(item))
        {
            currentItemCount--;
            inventoryItems.Remove(item);
            itemBagItems.Remove(item);
            Debug.Log("Item removed. Current item count: " + currentItemCount);
        }
        else
        {
            Debug.Log("Item not found in inventory!");
        }
    }
    /// <summary>
    /// Toggles the inventory UI.
    /// </summary>
    public void ToggleInventory()
    {
        if (uiManager != null)
        {
            if(uiManager.inventoryUI.activeSelf)
            {
                uiManager.SetUI(uiManager.hud);
                return;
            }
            uiManager.SetUI(uiManager.inventoryUI);
        }
    }
}
