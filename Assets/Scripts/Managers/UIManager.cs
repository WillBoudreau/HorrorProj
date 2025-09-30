using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject questLog;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject loadingScreen;

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
    }
    public void SetUI(GameObject uiElement)
    {
        SetFalseAllUI();
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }
}
