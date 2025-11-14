using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [Header("Door References")]
    [SerializeField] private HouseGenerator houseGenerator;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private GameObject interactionPrompt;
    public enum DoorType {Enter, Exit}
    [SerializeField] private DoorType doorType;
    public enum SpaceType { House, Store, Bakery, Park, School, Hospital, MainHouse }
    [SerializeField] private SpaceType spaceType;
    [SerializeField] private int houseType;

    void Start()
    {
        DetermineHouseType();
        
        houseGenerator = FindObjectOfType<HouseGenerator>();
        uiManager = FindObjectOfType<UIManager>();
        playerInteractions = FindObjectOfType<PlayerInteractions>();

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
        houseGenerator.houseType = ChooseHouseType();

        interactionPrompt.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        interactionPrompt.SetActive(true);
        if (other.CompareTag("Player") && playerInteractions.isInteracting)
        {
            if (doorType == DoorType.Enter)
            {
                if(spaceType == SpaceType.MainHouse)
                {
                    levelManager.LoadLevel("HouseMainScene");
                    uiManager.SetUI(uiManager.homeMainUI);
                }
                else if(spaceType == SpaceType.House)
                {
                    Debug.Log("Entering house type: " + ChooseHouseType());
                    levelManager.LoadLevel("HouseScene");
                    levelManager.LoadHouse(houseGenerator.houseType);
                }
            }
            else if (doorType == DoorType.Exit)
            {
                levelManager.LoadLevel("GameplayScene");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        interactionPrompt.SetActive(false);
    }
    /// <summary>
    /// Determines the house type
    /// </summary>
    private void DetermineHouseType()
    {
        houseType = Random.Range(0, 1);
    }
    /// <summary>
    /// Choose the specific house type
    /// </summary>
    private int ChooseHouseType()
    {
        switch (houseType)
        {
            case 1:
                houseGenerator.houseType = 1;
                break;
            case 2:
                houseGenerator.houseType = 2;
                break;
            case 3:
                houseGenerator.houseType = 3;
                break;
            default:
                houseGenerator.houseType = 1;
                break;
        }
        return houseGenerator.houseType;
    }
}
