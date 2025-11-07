using UnityEngine;
using UnityEngine.AI;

public class EnemySoldierMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 700f;// Speed at which the enemy soldier rotates
    public Transform[] patrolPoints;// Array of patrol points for the enemy soldier
    [SerializeField] private int currentPatrolIndex = 0;// Current index of the patrol point
    [SerializeField] private int maxPatrolPoints = 10;// Maximum number of patrol points
    [SerializeField] private NavMeshAgent navMeshAgent;// Reference to the NavMeshAgent component
    private void Start()
    {
        FindPatrolPoints();
    }
    private void FixedUpdate()
    {

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);

        // Move towards the patrol point
        MoveTowards(targetPoint);
        // Rotate to face the patrol point
        //RotateTowards(targetPoint);

        // Check if the enemy soldier is close enough to the patrol point to switch to the next one
        if (distanceToTarget < 1.0f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
    ///<summary>
    /// Find all of the patrol points in the scene and assign them to the patrolPoints array.
    /// </summary>
    public void FindPatrolPoints()
    {
        GameObject[] patrolPointObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");
        int pointsToAssign = Mathf.Min(patrolPointObjects.Length, maxPatrolPoints);
        patrolPoints = new Transform[pointsToAssign];

        for (int i = 0; i < pointsToAssign; i++)
        {
            patrolPoints[i] = patrolPointObjects[i].transform;
        }
    }
    /// <summary>
    /// Moves the enemy soldier towards the current patrol point.
    /// </summary>
    /// <param name="targetPoint">The target patrol point.</param>
    private void MoveTowards(Transform targetPoint)
    {
       navMeshAgent.SetDestination(targetPoint.position);
    }
    // /// <summary>
    // /// Rotates the enemy soldier to face the current patrol point.
    // /// </summary>
    // /// <param name="targetPoint">The target patrol point.</param>
    // private void RotateTowards(Transform targetPoint)
    // {
    //     Vector3 direction = (targetPoint.position - transform.position).normalized;
    //     Quaternion lookRotation = Quaternion.LookRotation(direction);
    //     Quaternion smoothedRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
    //     rb.MoveRotation(smoothedRotation);
    // }

}
