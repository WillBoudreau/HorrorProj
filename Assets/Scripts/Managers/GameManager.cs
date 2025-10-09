using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;
    public UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindWithTag("Player");
        SetPlayerBehaviourFalse();
        uiManager.SetUI(uiManager.mainMenu);
    }

    public void SetPlayerBehaviourFalse()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerLook>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<PlayerInteractions>().enabled = false;
    }
    public void SetPlayerBehaviourTrue()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<PlayerLook>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlayerInteractions>().enabled = true;
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
