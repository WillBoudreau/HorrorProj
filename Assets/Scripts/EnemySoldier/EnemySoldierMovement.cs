
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldierMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 700f;// Speed at which the enemy soldier rotates
    [SerializeField] private int currentPatrolIndex = 0;// Current index of the patrol point
    [SerializeField] private int maxPatrolPoints = 10;// Maximum number of patrol points
    [SerializeField] private NavMeshAgent navMeshAgent;// Reference to the NavMeshAgent component
    [SerializeField] private float targetPointRadius = 1.5f;// Radius for the enemy to 
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;// Array of patrol points for the enemy soldier
    public Transform targetPoint;// Current target patrol point
    [SerializeField] private float distFromNearestSoldier = 5f;// Minimum distance to maintain from other enemy soldiers
    private void Start()
    {
        FindPatrolPoints();
    }
    private void FixedUpdate()
    {

        targetPoint = patrolPoints[currentPatrolIndex];
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);


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
        OrderPatrolPoints(patrolPointObjects);
    }
    ///<summary>
    /// Orders the patrol points based on the number in their name. From "PatrolPoint1" to "PatrolPointN".
    /// </summary>
    /// <param name="patrolPointObjects">Array of patrol point GameObjects.</param>
    private void OrderPatrolPoints(GameObject[] patrolPointObjects)
    {
        patrolPoints = new Transform[Mathf.Min(patrolPointObjects.Length, maxPatrolPoints)];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            string pointName = "PatrolPoint" + (i + 1);
            foreach (GameObject obj in patrolPointObjects)
            {
                if (obj.name == pointName)
                {
                    patrolPoints[i] = obj.transform;
                    break;
                }
            }
        }
    }
    /// <summary>
    /// Moves the enemy soldier towards the current patrol point.
    /// </summary>
    /// <param name="targetPoint">The target patrol point.</param>
    public void MoveTowards(Transform targetPoint)
    {
        navMeshAgent.SetDestination(targetPoint.position);
    }
    /// <summary>
    /// Checks for when a soldier is out of range to maintain distance from other soldiers.And moves them together if they are too far apart.
    /// </summary>
    public void MaintainDistWithSoldiers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distFromNearestSoldier);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("EnemySoldier") && hitCollider.gameObject != this.gameObject)
            {
                Vector3 directionToSoldier = hitCollider.transform.position - transform.position;
                float distanceToSoldier = directionToSoldier.magnitude;

                if (distanceToSoldier > distFromNearestSoldier)
                {
                    Vector3 moveDirection = directionToSoldier.normalized;
                    Vector3 newPosition = transform.position + moveDirection * (distanceToSoldier - distFromNearestSoldier);
                    navMeshAgent.SetDestination(newPosition);
                }
                else
                {
                    MoveTowards(targetPoint);
                }
            }
        }
    }
    /// <summary>
    /// Finds a specific target point that the enemy soldier should move towards. Ignores patrol points.
    /// </summary>
    public Transform TargetPoint()
    {
        // Find a specific target point that is not a patrol point and within the targetPointRadius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetPointRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("TargetPoint"))
            {
                return hitCollider.transform;
            }
        }
        return null;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distFromNearestSoldier);
    }
}
