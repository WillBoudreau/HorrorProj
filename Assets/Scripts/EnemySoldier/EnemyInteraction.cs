
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]

    [SerializeField] private bool hasGun = false;

    [Header("Ranges")]
    [SerializeField] private int detectionRange = 10;
    [SerializeField] private int attackRange = 2;
    [SerializeField] private float interactionRange = 2f;
    [Header("References")]
    [SerializeField] private GameObject grabPoint;
    [SerializeField] private GameObject gunObject;
    void Update()
    {
        //ChangeState();
    }
    // /// <summary>
    // /// Changes the enemy state based on the player's distance.
    // /// </summary>
    // private void ChangeState()
    // {
    //     GameObject player = GameObject.FindGameObjectWithTag("Player");
    //     if (player != null)
    //     {
    //         float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    //         if (distanceToPlayer <= detectionRange)
    //         {
    //             currentState = EnemyState.Alert;
    //             Search();   
    //         }
    //         else if (distanceToPlayer <= attackRange && hasGun)
    //         {
    //             currentState = EnemyState.Attacking;
    //         }
    //         else if(gunObject == null)
    //         {
    //             currentState = EnemyState.Searching;
    //             Search();
    //         }
    //         else if(distanceToPlayer > detectionRange)
    //         {
    //             currentState = EnemyState.Idle;
    //         }
    //     }
    // }
    public void Attack()
    {
        grabPoint.GetComponentInChildren<GunBehaviour>().FireGun();
    }
    private void Interact()
    {
        if (gunObject == null)
        {
            Debug.Log("No gun to interact with.");
            return;
        }
    }
    public void Search()
    {
        if (gunObject != null)
        {
            Debug.Log("Searching for player...");
        }
        else
        {
            if (Vector3.Distance(transform.position, gunObject.transform.position) <= interactionRange)
            {
                Debug.Log("Gun found! Picking up gun.");
                gunObject.transform.SetParent(grabPoint.transform);
                gunObject.transform.localPosition = Vector3.zero;
                gunObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}