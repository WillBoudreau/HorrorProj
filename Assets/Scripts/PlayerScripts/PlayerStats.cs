using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int maxHunger = 100;
    public int currentHunger;
    public int maxThirst = 100;
    public int currentThirst;

    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }
    /// <summary>
    /// Handles player death.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player has died.");
        // Implement death behavior (e.g., respawn, game over screen)
    }
    public void Eat(int foodValue)
    {
        currentHunger += foodValue;
        if (currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
    }
    public void Drink(int waterValue)
    {
        currentThirst += waterValue;
        if (currentThirst > maxThirst)
        {
            currentThirst = maxThirst;
        }
    }    
}
