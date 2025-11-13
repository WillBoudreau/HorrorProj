using UnityEngine;

public class EnemySoldierBehaviour : MonoBehaviour
{

    public enum EnemyState
    {
        Idle,
        Searching,
        Patrolling,
        Alert,
        Attacking
    }
    public EnemyState enemyState;
    [Header("Behaviour Settings")]
    [SerializeField] private EnemyInteraction enemyInteraction;
    [SerializeField] private EnemySoldierMovement enemyMovement;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    void Start()
    {
        enemyInteraction = GetComponent<EnemyInteraction>();
        enemyMovement = GetComponent<EnemySoldierMovement>();

        currentHealth = maxHealth;

        enemyState = EnemyState.Patrolling;
    }
    void Update()
    {
        SetState();
    }
    /// <summary>
    /// Set the enemy state
    /// </summary>
    void SetState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                enemyMovement.ScanArea();
                break;
            case EnemyState.Searching:
                // enemyMovement.MoveTowards(enemyMovement.TargetPoint());
                enemyInteraction.Search();
                break;
            case EnemyState.Patrolling:
                enemyMovement.MaintainDistWithSoldiers();
                break;
            case EnemyState.Alert:
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    enemyMovement.MoveTowards(player.transform);
                }
                break;
            case EnemyState.Attacking:
                enemyInteraction.Attack();
                break;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Enemy took {damageAmount} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
