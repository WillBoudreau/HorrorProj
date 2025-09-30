using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    [Header("House References")]
    [SerializeField] private List<GameObject> housePrefabs;
    [SerializeField] private Transform houseSpawnPoint;

    public void GenerateHouse()
    {
        if (housePrefabs.Count == 0 || houseSpawnPoint == null)
        {
            Debug.LogError("House Prefabs or Spawn Point not set.");
            return;
        }

        int randomIndex = Random.Range(0, housePrefabs.Count);
        GameObject selectedHouse = housePrefabs[randomIndex];
        Instantiate(selectedHouse, houseSpawnPoint.position, houseSpawnPoint.rotation);
    }
}
