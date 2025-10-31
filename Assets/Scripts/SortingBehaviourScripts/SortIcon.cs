using UnityEngine;

public class SortIcon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sortable"))
        {
            Debug.Log("Sortable object entered the sort area: " + other.name);
            // Implement sorting logic here
        }
    }
}
