using UnityEngine;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    [Header("Item Generation Settings")]
    [SerializeField] private LevelManager levelManager;// Reference to the LevelManager
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private List<GameObject> spawnedItems = new List<GameObject>();
    [SerializeField] private float spawnInterval = 0.025f;
    [SerializeField] private int maxFoodItems = 10;
    [Header("Item spawn zone settings")]
    [SerializeField] private Vector3 spawnZoneCenter;
    [SerializeField] private Vector3 spawnZoneSize;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

    }
    void FixedUpdate()
    {
        if(levelManager.levelName == "GameplayScene")
        {
            InvokeRepeating("SpawnItems", 2f, spawnInterval);
        }
    }

    /// <summary>
    /// Spawns the food items around the map at random locations
    /// </summary>
    public void SpawnItems()
    {
        int currentFoodCount = GameObject.FindGameObjectsWithTag("Food").Length;
        if (currentFoodCount >= maxFoodItems) return;

        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        Vector3 randomPosition = PickSpawnPoint();

        // Check if the position is valid for spawning
        while (spawnedItems.Count < maxFoodItems)
        {
            if (CanSpawnItem(randomPosition))
            {
                GameObject newItem = Instantiate(itemPrefab, randomPosition, Quaternion.identity);
                spawnedItems.Add(newItem);
                Debug.Log("Spawned item at: " + randomPosition);
                break;
            }
            else
            {
                // Generate a new random position if the current one is not valid
                randomPosition = PickSpawnPoint();
            }
        }
    }
    /// <summary>
    /// Picks a spawn point
    /// </summary>
    private Vector3 PickSpawnPoint()
    {
        // Generate a random position within the spawn zone
        Vector3 randomPosition = spawnZoneCenter + new Vector3(
            Random.Range(-spawnZoneSize.x, spawnZoneSize.x),
            0,
            Random.Range(-spawnZoneSize.z, spawnZoneSize.z)
        );
        return randomPosition;
    }
    /// <summary>
    /// Checks if the item can be spawned at the given position to make sure it is not inside a wall or other obstacle
    /// </summary>
    private bool CanSpawnItem(Vector3 position)
    {
        // Check if the position is not inside a wall or other obstacle
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("House") || collider.CompareTag("Food"))
            {
                return false;
            }
        }
        return true;
    }
}
