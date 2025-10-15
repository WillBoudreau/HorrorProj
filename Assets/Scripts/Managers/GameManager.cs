using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;
    public UIManager uiManager;
    [SerializeField] private AdjustSettings adjustSettings;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindWithTag("Player");
        SetPlayerBehaviourFalse("All");
        uiManager.SetUI(uiManager.mainMenu);
        adjustSettings = FindObjectOfType<AdjustSettings>();

        StartGame();
    }

    public void SetPlayerBehaviourFalse(string behaviour)
    {
        switch (behaviour)
        {
            case "Movement":
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case "LookW/Cursor":
                player.GetComponentInChildren<PlayerLook>().enabled = false;
                break;
            case "Interactions":
                player.GetComponent<PlayerInteractions>().enabled = false;
                break;
            case "All":
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponentInChildren<PlayerLook>().enabled = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<PlayerInteractions>().enabled = false;
                break;
            case "LookW/O Cursor":
                player.GetComponentInChildren<PlayerLook>().enabled = false;
                if (Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                break;
            default:
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponentInChildren<PlayerLook>().enabled = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<PlayerInteractions>().enabled = false;
                break;
        }
    }
    public void SetPlayerBehaviourTrue()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<PlayerLook>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlayerInteractions>().enabled = true;
    }
    /// <summary>
    /// Initializes the game
    /// </summary>
    public void StartGame()
    {
        SetPlayerBehaviourTrue();
        uiManager.SetUI(uiManager.mainMenu);
        Settings.playerSensitivity = adjustSettings.defaultSensitivity;
        //adjustSettings.OnSensitivityChanged(adjustSettings.defaultSensitivity);
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
