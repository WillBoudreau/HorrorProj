
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldierMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float rotationSpeed = 15f;// Speed at which the enemy soldier rotates
    [SerializeField] private int currentPatrolIndex = 0;// Current index of the patrol point
    [SerializeField] private int maxPatrolPoints = 10;// Maximum number of patrol points
    [SerializeField] private NavMeshAgent navMeshAgent;// Reference to the NavMeshAgent component
    [SerializeField] private float targetPointRadius = 1.5f;// Radius for the enemy to 
    public bool guardingPoint = false;// Whether the enemy soldier is guarding a specific point
    public bool soldiersNearby = false;// Whether there are other soldiers nearby
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;// Array of patrol points for the enemy soldier
    public Transform targetPoint;// Current target patrol point
    [SerializeField] private float maxDistFromNearestSoldier = 5f;// Minimum distance to maintain from other enemy soldiers
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistFromNearestSoldier);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("EnemySoldier") && hitCollider.gameObject != this.gameObject)
            {
                guardingPoint = false;
                soldiersNearby = true;
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (distance > maxDistFromNearestSoldier)
                {
                    Debug.Log("Moving closer to nearby soldier.");
                    soldiersNearby = false;
                    navMeshAgent.SetDestination(hitCollider.transform.position);
                }
                else
                {
                    Debug.Log("Maintaining position near nearby soldier.");
                    navMeshAgent.SetDestination(targetPoint.position);
                }
            }
            else if(!hitCollider.CompareTag("EnemySoldier") && soldiersNearby == false)
            {
                guardingPoint = true;
                GuardPoint();
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
    /// <summary>
    /// When the enemy isn't moving, stand still, but scan the area.
    /// </summary>
    public void ScanArea()
    {
        navMeshAgent.SetDestination(transform.position);
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    /// <summary>
    /// When guarding a point, find the nearest cover and move towards it.
    /// </summary>
    private void GuardPoint()
    {
        if (guardingPoint)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetPointRadius);
            Transform nearestCover = null;
            float nearestDistance = Mathf.Infinity;

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Cover"))
                {
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestCover = hitCollider.transform;
                    }
                }
            }
            if (nearestCover != null)
            {
                navMeshAgent.SetDestination(nearestCover.position);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxDistFromNearestSoldier);
    }
}