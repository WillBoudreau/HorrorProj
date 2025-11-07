using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Alert,
        Attacking
    }
    public EnemyState currentState = EnemyState.Idle;
    [Header("Interaction Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private int detectionRange = 10;
    [SerializeField] private int attackRange = 2;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        ChangeState();
    }
    /// <summary>
    /// Changes the enemy state based on the player's distance.
    /// </summary>
    private void ChangeState()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                currentState = EnemyState.Alert;
            }
            else if (distanceToPlayer <= attackRange)
            {
                currentState = EnemyState.Attacking;
            }
            else
            {
                currentState = EnemyState.Idle;
            }
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
