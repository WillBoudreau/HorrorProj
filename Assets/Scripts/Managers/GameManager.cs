using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game References")]
    public GameObject player;

    public void SetPlayerBehaviourFalse()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    public void SetPlayerBehaviourTrue()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
