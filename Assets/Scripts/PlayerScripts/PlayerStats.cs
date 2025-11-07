
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxHealth = 100;
    public float currentHealth;
    public float maxHunger = 100;
    public float currentHunger;
    public float maxThirst = 100;
    public float currentThirst;
    public bool isDead = false;
    [Header("Player Stats references")]
    public PlayerDeathStats deathStats;
    public FamilyMemberBehaviour familyMemberBehaviour;

    void Start()
    {
        deathStats = FindObjectOfType<PlayerDeathStats>();

        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;

        isDead = false;
    }
    ///<summary>
    /// Update the stats based on the family member behaviour.
    /// </summary>
    void Update()
    {
        if (familyMemberBehaviour != null)
        {
            currentHunger = familyMemberBehaviour.hungerLevel;
            currentThirst = familyMemberBehaviour.thirstLevel;
        }
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
