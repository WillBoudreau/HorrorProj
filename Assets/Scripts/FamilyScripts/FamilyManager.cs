using System.ComponentModel;
using UnityEngine;
using System;
using System.Collections.Generic;

public class FamilyManager: MonoBehaviour
{
  [Header("Family Management")]
  public List<FamilyMemberBehaviour> familyMembers = new List<FamilyMemberBehaviour>();
  /// <summary>
  /// Find all the family members in the scene and add them to the familyMembers list.
  /// </summary>
  public void FindFamilyMembersInScene()
  {
    familyMembers.Clear();
    FamilyMemberBehaviour[] membersInScene = FindObjectsOfType<FamilyMemberBehaviour>();
    familyMembers.AddRange(membersInScene);
  }
  /// <summary>
  /// Get food from the player's inventory to replenish the family's food supply.
  /// </summary>
  public void GetFoodFromInventory(float amount)
  {
    Settings.foodSupply += amount;
    Settings.foodSupply = Mathf.Clamp(Settings.foodSupply, 0, 100);
  }
  /// <summary>
  /// Get water from the player's inventory to replenish the family's water supply.
  /// </summary>
  public void GetWaterFromInventory(float amount)
  {
    Settings.waterSupply += amount;
    Settings.waterSupply = Mathf.Clamp(Settings.waterSupply, 0, 100);
  }
  /// <summary>
  /// At the end of each day, divide the food and water supply among family members. Depending on how the player chooses
  /// to divide the resources, the family members will have different health outcomes.
  /// </summary>
  /// <param name="foodPerMember">The amount of food to give each member (0 to 100 percentage).</param>
  /// <param name="waterPerMember">The amount of water to give each member (
  /// 0 to 100 percentage).</param>
  /// <returns>True if all family members are healthy or hungry, false if any member is sick.</returns>
  /// <remarks>This method assumes that foodPerMember and waterPerMember are percentages (0 to 100).</remarks>
  public bool DistributeResources(float foodPerMember, float waterPerMember)
  {
    foreach (var member in familyMembers)
    {
      bool isHealthy = member.UpdateStatus(Settings.dayLengthInSeconds, foodPerMember, waterPerMember); // Simulate a full day
      if (!isHealthy)
      {
        return false;
      }
    }
    return true;
  }
}
