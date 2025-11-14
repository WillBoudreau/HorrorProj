using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


//using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;
    public PlayerStats playerStats;
    public PlayerDeathStats playerDeathStats;
    public UIManager uiManager;
    [SerializeField] private FamilyManager familyManager;
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] private AdjustSettings adjustSettings;
    [SerializeField] private LoadingScreenBehaviour loadingScreenBehaviour;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        familyManager = FindObjectOfType<FamilyManager>();
        loadingScreenBehaviour = FindObjectOfType<LoadingScreenBehaviour>();
        //familyMembers = new List<FamilyMemberBehaviour>(FindObjectsOfType<FamilyMemberBehaviour>());

        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerDeathStats = FindObjectOfType<PlayerDeathStats>();
        SetPlayerBehaviourFalse("All");
        uiManager.SetUI(uiManager.mainMenu);
        adjustSettings = FindObjectOfType<AdjustSettings>();

        StartGame();
    }
    void Update()
    {
        SetGameOver();
    }

    public void SetGameOver()
    {
        if(playerStats.isDead)
        {
            SetPlayerBehaviourFalse("All");
            uiManager.SetUI(uiManager.gameOverScreen);
        }
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
        if(!loadingScreenBehaviour.isLoading)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponentInChildren<PlayerLook>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<PlayerInteractions>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    ///<summary>
    /// move to the next day
    ///</summary>
    public void MoveToNextDay()
    {
        // Move the day/night cycle forward
        //dayNightCycle.MoveToNextDay();

        familyManager.FindFamilyMembersInScene();

        // Update the status of all family members
        foreach (var member in familyManager.familyMembers)
        {
            if(member == null)
            {
                Debug.LogWarning($"Family member {member} is null.");
            }
            Debug.Log("Updating status for " + member.memberName);
            member.UpdateStatus(Settings.timeOfDay, member.hungerRate * Settings.foodConsumptionRate, member.thirstRate * Settings.waterConsumptionRate);
            Debug.Log(member.memberName + " status: " + member.healthStatus);
        }

        SetPlayerBehaviourTrue();

        // Reset time of day
        Settings.timeOfDay = 0f;
        uiManager.SetUI(uiManager.hud);
    }

    /// <summary>
    /// Initializes the game
    /// </summary>
    public void StartGame()
    {
        SetPlayerBehaviourTrue();
        uiManager.SetUI(uiManager.mainMenu);
        Settings.playerSensitivity = adjustSettings.defaultSensitivity;
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
