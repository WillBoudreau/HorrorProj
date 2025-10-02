using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SetPlayerBehaviourFalse();
    }

    public void SetPlayerBehaviourFalse()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerLook>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void SetPlayerBehaviourTrue()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<PlayerLook>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }
}
