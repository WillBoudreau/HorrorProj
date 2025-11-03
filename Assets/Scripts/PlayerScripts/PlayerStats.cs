
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
    public bool isDead = false;
    [Header("Player Stats references")]
    public PlayerDeathStats deathStats;

    void Start()
    {
        deathStats = FindObjectOfType<PlayerDeathStats>();

        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;

        isDead = false;
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
    public void Die()
    {
        Debug.Log("Player has died.");
        isDead = true;
        if (currentHealth <= 0)
        {
            deathStats.SetDeathMessage("Health Depleted");
        }
        else if (currentHunger <= 0)
        {
            deathStats.SetDeathMessage("Starvation");
        }
        else if (currentThirst <= 0)
        {
            deathStats.SetDeathMessage("Dehydration");
        }
        else
        {
            deathStats.SetDeathMessage("Unknown Forces");
        }
        Debug.Log("Death message set.");
        Debug.Log(currentHealth + " " + currentHunger + " " + currentThirst);
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
