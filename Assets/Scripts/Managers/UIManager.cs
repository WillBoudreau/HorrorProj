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
    [Header("Family UI Elements")]
    [SerializeField] private GameObject supplyButton;
    [SerializeField] private GameObject inventoryButton;

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
        loadingScreen.SetActive(false);
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
    /// Tweens in the specified UI element from one side to the screen and tweens out all other UI elements.
    /// </summary>
    public void TweenUI(GameObject uiElement)
    {
        SetFalseAllUI();
        if (uiElement != null)
        {
            if (uiElement.activeSelf == false)
            {
                uiElement.SetActive(true);
            }
            else
            {
                uiElement.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Family button set active or not
    /// </summary>
    public void SetFamilyButtonActive(GameObject button)
    {
        if (button == supplyButton)
        {
            supplyButton.SetActive(true);
            inventoryButton.SetActive(false);
        }
        else if (button == inventoryButton)
        {
            supplyButton.SetActive(false);
            inventoryButton.SetActive(true);
        }
        else
        {
            supplyButton.SetActive(false);
            inventoryButton.SetActive(false);
        }
    } 
}