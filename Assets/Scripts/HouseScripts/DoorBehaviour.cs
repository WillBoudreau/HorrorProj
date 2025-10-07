using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager; 
    public enum DoorType {Enter, Exit}
    [SerializeField] private DoorType doorType;

    void Start()
    {
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
                levelManager.lastPlayerSpawnPoint = this.gameObject.transform;
                levelManager.LoadLevel("HouseScene");
            }
            else if (doorType == DoorType.Exit)
            {
                levelManager.LoadLevel("GameplayScene");
            }
        }
    }
}
