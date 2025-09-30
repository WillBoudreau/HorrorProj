using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string levelName;
    [SerializeField] private int levelIndex;
    [SerializeField] private bool isLevelCompleted = false;

    public void LoadLevel(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.LogError($"Level name is not set: {name}");
        }
    }

}
