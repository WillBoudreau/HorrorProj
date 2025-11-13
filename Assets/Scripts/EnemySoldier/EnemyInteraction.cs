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
    public void Attack()
    {
        if (gunObject == null)
        {
            Debug.Log("No gun to interact with.");
            return;
        }
        else
        {
            grabPoint.GetComponentInChildren<GunBehaviour>().FireGun();
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