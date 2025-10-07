using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
