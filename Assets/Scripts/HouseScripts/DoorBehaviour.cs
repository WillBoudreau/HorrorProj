using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [Header("Door References")]
    [SerializeField] private HouseGenerator houseGenerator;
    [SerializeField] private LevelManager levelManager; 
    public enum DoorType {Enter, Exit}
    [SerializeField] private DoorType doorType;
    [SerializeField] private int houseType;

    void Start()
    {
        DetermineHouseType();
        houseGenerator = FindObjectOfType<HouseGenerator>();
        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorType == DoorType.Enter)
            {
                houseGenerator.houseType = houseType;
                levelManager.LoadLevel("HouseScene");
                levelManager.LoadHouse(houseType);
            }
            else if (doorType == DoorType.Exit)
            {
                levelManager.LoadLevel("GameplayScene");
            }
        }
    }
    /// <summary>
    /// Determines the house type
    /// </summary>
    private void DetermineHouseType()
    {
        houseType = Random.Range(0, 3);
    }
}
