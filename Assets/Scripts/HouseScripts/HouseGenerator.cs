using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    [Header("House References")]
    [SerializeField] private List<GameObject> housePrefabs;
    [SerializeField] private Transform houseSpawnPoint;
    public int houseType;

    public void FindHouseSpawnPoint()
    {
        if (houseSpawnPoint == null)
        {
            houseSpawnPoint = GameObject.FindGameObjectWithTag("HouseSpawnPoint").transform;
        }
    }

    public void GenerateHouse(int houseType)
    {
        if (housePrefabs.Count == 0 || houseSpawnPoint == null)
        {
            Debug.LogError("House Prefabs or Spawn Point not set.");
            return;
        }

        GameObject selectedHouse = housePrefabs[houseType];
        Instantiate(selectedHouse, houseSpawnPoint.position, houseSpawnPoint.rotation);
    }
}
