using UnityEngine;



public class FamilyMemberBehaviour : MonoBehaviour
{
  public string memberName;
  public Settings.HealthStatus healthStatus = new Settings.HealthStatus();

  void Start()
  {
    // Initialize member properties from Settings
    memberName = Settings.memberName;
    healthStatus = Settings.healthStatus;
  }
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
    Settings.hungerLevel += foodConsumed;
    Settings.thirstLevel += waterConsumed;

    // Clamp values between 0 and 100
    Settings.hungerLevel = Mathf.Clamp(Settings.hungerLevel, 0, 100);
    Settings.thirstLevel = Mathf.Clamp(Settings.thirstLevel, 0, 100);

    // Update health status based on hunger and thirst levels
    if (Settings.hungerLevel < 20 || Settings.thirstLevel < 20)
    {
      healthStatus = Settings.HealthStatus.Sick;
      return false; // Member is sick
    }
    else if (Settings.hungerLevel < 50 || Settings.thirstLevel < 50)
    {
      healthStatus = Settings.HealthStatus.Hungry;
      return true; // Member is hungry but not sick
    }
    else
    {
      healthStatus = Settings.HealthStatus.Healthy;
      return true; // Member is healthy
    }
  }
  /// <summary>
  /// When a family member is clicked, display their status.
  /// </summary>
  void OnMouseDown()
  {
    Debug.Log($"Member: {memberName}, Health: {healthStatus}, Hunger: {Settings.hungerLevel}, Thirst: {Settings.thirstLevel}");
  }
  /// <summary>
  /// When a family member is sick for too long, they may die.
  /// </summary>
  public void Die()
  {
    Debug.Log($"{memberName} has died.");
    Destroy(gameObject);
  }

}
