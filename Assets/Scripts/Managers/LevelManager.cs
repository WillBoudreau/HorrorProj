using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level References")]
    [SerializeField] private GameManager gameManager;// Reference to the GameManager
    [SerializeField] private HouseGenerator houseGenerator;// Reference to the HouseGenerator
    public Transform lastPlayerSpawnPoint;// Last player spawn point
    [Header("Level Settings")]
    public string levelName;// Name of the level to load
    [SerializeField] private int levelIndex;// Index of the level in build settings
    [SerializeField] private Transform playerSpawnPoint;// Player spawn point in the level
    public bool isLevelLoaded = false;// Level loading status
    public List<AsyncOperation> scenesLoading = new List<AsyncOperation>();// List of scenes currently loading
    [SerializeField] private float sceneLoadTimeout = 5f;// Timeout for scene loading
    private float sceneLoadTimer = 0f;// Timer to track scene loading time

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        houseGenerator = FindObjectOfType<HouseGenerator>();
    }
    /// <summary>
    /// Loads a level by name asynchronously and tracks its loading state.
    /// </summary>
    /// <param name="name"></param>
    public void LoadLevel(string name)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        if (name == "MainMenuScene")
        {
            gameManager.SetPlayerBehaviourFalse();
        }
        else if (name == "GameplayScene")
        {
            gameManager.SetPlayerBehaviourTrue();
        }
        else if (asyncLoad != null)
        {
            scenesLoading.Add(asyncLoad);
            StartCoroutine(TrackSceneLoading(asyncLoad));
            Debug.Log("Loading scene: " + name);
        }
    }
    /// <summary>
    /// Tracks the loading progress of a scene and handles timeout.
    /// </summary>
    /// <param name="asyncLoad"></param>
    /// <returns></returns>
    private IEnumerator TrackSceneLoading(AsyncOperation asyncLoad)
    {
        sceneLoadTimer = 0f;
        while (!asyncLoad.isDone)
        {
            sceneLoadTimer += Time.deltaTime;
            if (sceneLoadTimer >= sceneLoadTimeout)
            {
                Debug.LogError("Scene loading timed out.");
                yield break;
            }

            yield return null;
        }
        Debug.Log("Scene loaded successfully.");
        isLevelLoaded = true;
        scenesLoading.Remove(asyncLoad);
    }
    /// <summary>
    /// Callback when a scene is loaded.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        if (scene.name == "HouseScene")
        {
            mode = LoadSceneMode.Additive;
            LoadHouse(houseGenerator.houseType);
        }
        levelName = scene.name;
        FindPlayerSpawn();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    ///<summary>
    /// Loads the house within the level.
    /// </summary>
    /// <param name="houseType"></param>
    public void LoadHouse(int houseType)
    {
        houseGenerator.FindHouseSpawnPoint();
        houseGenerator.GenerateHouse(houseType);
    }

    /// <summary>
    /// Finds and returns the player spawn point in the level.
    /// </summary>
    public Transform FindPlayerSpawn()
    {
        if (lastPlayerSpawnPoint == null)
        {
            if (playerSpawnPoint == null)
            {
                GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
                playerSpawnPoint = spawnPoint.transform;
                SetPlayerAtSpawnPoint(gameManager.player.transform);
            }
            else if (playerSpawnPoint != null)
            {
                SetPlayerAtSpawnPoint(gameManager.player.transform);
            }
            else
            {
                Debug.LogWarning("Player Spawn Point not found. Using default position.");
            }
        }
        else if (lastPlayerSpawnPoint != null)
        {
            playerSpawnPoint = lastPlayerSpawnPoint;
            SetPlayerAtSpawnPoint(gameManager.player.transform);
            lastPlayerSpawnPoint = null;
        }
        return playerSpawnPoint;
    }
    /// <summary>
    /// Set the player at the spawn point.
    /// </summary>
    public void SetPlayerAtSpawnPoint(Transform player)
    {
        if (playerSpawnPoint != null)
        {
            player.position = playerSpawnPoint.position;
            player.rotation = playerSpawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("Player spawn point not found. Using default position.");
        }
    }
}