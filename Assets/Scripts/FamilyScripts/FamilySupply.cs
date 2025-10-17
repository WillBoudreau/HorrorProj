using UnityEngine;
using System.Collections.Generic;


public class FamilySupply : MonoBehaviour
{
    [Header("Supply references")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private List<GameObject> familyMembers = new List<GameObject>();
    [SerializeField] private List<InteractableOBJ.PickupType> foodItems = new List<InteractableOBJ.PickupType>();
    [SerializeField] private List<InteractableOBJ.PickupType> waterItems = new List<InteractableOBJ.PickupType>();
    [SerializeField] private List<InteractableOBJ.PickupType> medicineItems = new List<InteractableOBJ.PickupType>();


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateSupplyUI();
    }
    /// <summary>
    /// Adds a specified amount of items to the corresponding supply.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="amount"></param>
    public void AddToSupply(InteractableOBJ.PickupType itemType, int amount)
    {
        switch (itemType)
        {
            case InteractableOBJ.PickupType.Food:
                Settings.numOfFoodItems += amount;
                Debug.Log("Food items in supply: " + Settings.numOfFoodItems);
                uiManager.UpdateSupplyUI();
                UpdateItemList(itemType);
                break;
            case InteractableOBJ.PickupType.Water:
                Settings.numOfWaterItems += amount;
                uiManager.UpdateSupplyUI();
                UpdateItemList(itemType);
                break;
            case InteractableOBJ.PickupType.Medicine:
                Settings.numOfMedicineItems += amount;
                uiManager.UpdateSupplyUI();
                UpdateItemList(itemType);
                break;
            default:
                Debug.Log("Invalid item type");
                break;
        }
    }
    /// <summary>
    /// Updates the corresponding supply
    /// </summary>
    void UpdateItemList(InteractableOBJ.PickupType itemType)
    {
        Debug.Log("Updating item list for: " + itemType.ToString());
        foodItems.Clear();
        waterItems.Clear();
        medicineItems.Clear();
        switch (itemType)
        {
            case InteractableOBJ.PickupType.Food:
                foodItems.Add(itemType);
                break;
            case InteractableOBJ.PickupType.Water:
                waterItems.Add(itemType);
                break;
            case InteractableOBJ.PickupType.Medicine:
                medicineItems.Add(itemType);
                break;
            default:
                Debug.Log("Invalid item type");
                break;
        }
    }
}