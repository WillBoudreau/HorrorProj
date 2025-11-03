using UnityEngine;

public class FamilyMemberBehaviour : MonoBehaviour
{
  public string memberName;
  public enum HealthStatus { Healthy, Hungry, Sick, Recovering, Dead }
  public  HealthStatus healthStatus;
  public float hungerLevel = 100.0f; // 0 to 100
  public float thirstLevel = 100.0f; // 0 to 100
  public float hungerRate = 1.0f; // Rate at which hunger decreases
  public float thirstRate = 1.5f; // Rate at which thirst decreases
  [Header("Family level thresholds")]
  public float sickThreshold = 20.0f;
  public float hungryThreshold = 50.0f;
  public float recoveryThreshold = 70.0f;
  public float deathThreshold = 0.0f;

  /// <summary>
  /// Updates the family members status based on the amount of time, water and food they have.
  /// </summary>
  /// <param name="timePassed">The amount of time passed in seconds.</param>
  /// <param name="foodConsumed">The amount of food consumed (0 to 100
  /// percentage).</param>
  /// <param name="waterConsumed">The amount of water consumed (0 to 100
  /// percentage).</param>
  public bool UpdateStatus(float timePassed, float foodConsumed, float waterConsumed)
  {
    // Increase hunger and thirst levels based on consumption
    hungerLevel -= foodConsumed;
    thirstLevel -= waterConsumed;

    // Clamp values between 0 and 100
    hungerLevel = Mathf.Clamp(hungerLevel, 0, 100);
    thirstLevel = Mathf.Clamp(thirstLevel, 0, 100);

    // Update health status based on hunger and thirst levels
    if (hungerLevel < sickThreshold && hungerLevel > deathThreshold || thirstLevel < sickThreshold && thirstLevel > deathThreshold)
    {
      healthStatus = HealthStatus.Sick;
      Debug.Log($"{memberName} - Hunger: {hungerLevel}, Thirst: {thirstLevel}, Status: {healthStatus}");
      return false; // Member is sick
    }
    else if (hungerLevel < hungryThreshold && hungerLevel > deathThreshold || thirstLevel < hungryThreshold && thirstLevel > deathThreshold)
    {
      healthStatus = HealthStatus.Hungry;
      Debug.Log($"{memberName} - Hunger: {hungerLevel}, Thirst: {thirstLevel}, Status: {healthStatus}");
      return true; // Member is hungry but not sick
    }
    else if (hungerLevel <= deathThreshold || thirstLevel <= deathThreshold)
    {
      healthStatus = HealthStatus.Dead;
      Die();
      return false; // Member has died
    }
    else
    {
      healthStatus = HealthStatus.Healthy;
      Debug.Log($"{memberName} - Hunger: {hungerLevel}, Thirst: {thirstLevel}, Status: {healthStatus}");
      return true; // Member is healthy
    }
  }
  /// <summary>
  /// When a family member is sick for too long, they may die.
  /// </summary>
  public void Die()
  {
    if (memberName == "Player")
    {
      PlayerStats playerStats = FindObjectOfType<PlayerStats>();
      if (playerStats != null)
      {
        playerStats.Die();
      }
      else
      {
        Debug.LogError("PlayerStats component not found in the scene.");
      }
    }
    else
    {
      Debug.Log($"{memberName} has died due to poor health.");
      Destroy(gameObject);
    }
  }

}