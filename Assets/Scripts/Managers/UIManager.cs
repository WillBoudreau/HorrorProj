using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;



public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject hud;
    public GameObject inventoryUI;
    public GameObject settingsMenu;
    public GameObject dialogueBox;
    public GameObject questLog;
    public GameObject mapUI;
    public GameObject loadingScreen;
    public GameObject homeMainUI;

    [Header("Loading Screen")]
    [SerializeField] private LoadingScreenBehaviour loadingScreenBehaviour;

    [Header("Family UI Elements")]
    [SerializeField] private GameObject supplyButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject supplyPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Slider foodSupplySlider;
    [SerializeField] private Slider waterSupplySlider;
    [SerializeField] private Slider medicineSupplySlider;

    public void SetFalseAllUI()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverScreen.SetActive(false);
        hud.SetActive(false);
        inventoryUI.SetActive(false);
        settingsMenu.SetActive(false);
        dialogueBox.SetActive(false);
        questLog.SetActive(false);
        mapUI.SetActive(false);
        //loadingScreen.SetActive(false);
        homeMainUI.SetActive(false);
    }
    /// <summary>
    /// Sets the specified UI element as active and hides all other UI elements.
    /// </summary>
    /// <param name="uiElement">The UI element to activate.</param>
    public void SetUI(GameObject uiElement)
    {
        SetFalseAllUI();
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }
    /// <summary>
    /// Family button set active or not
    /// </summary>
    public void SetFamilyButtonActive(GameObject button)
    {
        if (button == supplyButton)
        {
            Debug.Log("Supply button clicked");
            supplyPanel.SetActive(true);
            inventoryPanel.SetActive(false);
            supplyButton.SetActive(false);
            inventoryButton.SetActive(true);
        }
        else if (button == inventoryButton)
        {
            Debug.Log("Inventory button clicked");
            supplyPanel.SetActive(false);
            inventoryPanel.SetActive(true);
            supplyButton.SetActive(true);
            inventoryButton.SetActive(false);
        }
    }
    /// <summary>
    /// Updates the supply UI sliders.
    /// </summary>
    public void UpdateSupplyUI()
    {
        Debug.Log("Updating supply UI...");
        if (foodSupplySlider != null)
        {
            foodSupplySlider.maxValue = Settings.maxFoodItems;
            foodSupplySlider.value = Settings.numOfFoodItems;
            Debug.Log("Food supply slider updated: " + Settings.numOfFoodItems);
            Debug.Log(foodSupplySlider.value);
        }
        if (waterSupplySlider != null)
        {
            waterSupplySlider.maxValue = Settings.maxWaterItems;
            waterSupplySlider.value = Settings.numOfWaterItems;
        }
        if (medicineSupplySlider != null)
        {
            medicineSupplySlider.maxValue = Settings.maxMedicineItems;
            medicineSupplySlider.value = Settings.numOfMedicineItems;
        }
    }
    /// <summary>
    /// Updates the loading screen progress bar.
    /// </summary>
    public void UpdateLoadingScreen()
    {
        Debug.Log("Updating loading screen...");
        if (loadingScreenBehaviour != null)
        {
            Debug.Log("Starting loading sequence...");
            StartCoroutine(loadingScreenBehaviour.StartLoadingSequence());
            Debug.Log("Loading sequence started.");
        }
    }
}