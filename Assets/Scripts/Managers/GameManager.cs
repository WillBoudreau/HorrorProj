using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private HouseGenerator houseGenerator;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        if (houseGenerator == null)
        {
            houseGenerator = FindObjectOfType<HouseGenerator>();
        }
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }
}
