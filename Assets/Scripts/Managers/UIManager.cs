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
    public GameObject familySortUI;

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

    [Header("Family Status Elements")]
    public TextMeshProUGUI familyMemberNameText;
    public TextMeshProUGUI familyMemberHealthText;
    public TextMeshProUGUI familyMemberHungerText;
    public TextMeshProUGUI familyMemberThirstText;

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
        familySortUI.SetActive(false);
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
    /// <summary>
    /// Updates the family member status UI. Depending on the selected family member, it updates the name, health, hunger, and thirst texts.
    /// </summary>
    public void UpdateFamilyMemberStatusUI(FamilyMemberBehaviour selectedMember)
    {
        if (selectedMember != null)
        {
            familyMemberNameText.text = selectedMember.memberName;
            familyMemberHungerText.text = "Hunger: " + selectedMember.hungerLevel.ToString();
            familyMemberThirstText.text = "Thirst: " + selectedMember.thirstLevel.ToString();
            familyMemberHealthText.text = "Health: " + selectedMember.healthStatus.ToString();
        }
        else
        {
            familyMemberNameText.text = "No Member Selected";
            familyMemberHungerText.text = "Hunger:";
            familyMemberThirstText.text = "Thirst:";
            familyMemberHealthText.text = "Health Status:";
        }
    }
}