using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class FamilyMemberOrder : MonoBehaviour
{
    [Header("Family Member Order")]
    [SerializeField] private FamilyManager familyManager;
    [SerializeField] private FamilySupply familySupply;
    [SerializeField] private List<FamilyMemberBehaviour> familyMembers = new List<FamilyMemberBehaviour>();
    [SerializeField] private List<TMP_Dropdown> memberDropdowns = new List<TMP_Dropdown>();
    public int orderIndex;

    // Start is called before the first frame update
    void Start()
    {
        familyManager = FindObjectOfType<FamilyManager>();
        familySupply = FindObjectOfType<FamilySupply>();

        FillFamilyMembersList();
        UpdateDropdownOptions();


        // Initialize the order index
        orderIndex = 0;
    }
    /// <summary>
    /// Get the next family member in the order.
    /// </summary>
    public FamilyMemberBehaviour GetNextFamilyMember()
    {
        if (familyMembers.Count == 0)
        {
            return null;
        }

        FamilyMemberBehaviour nextMember = familyMembers[orderIndex];
        orderIndex = (orderIndex + 1) % familyMembers.Count; // Loop back to the start
        return nextMember;
    }
    /// <summary>
    /// Check the supplies for the next family member in order.
    /// </summary>
    public void CheckNextMemberSupplies()
    {
        FamilyMemberBehaviour member = GetNextFamilyMember();
        if (CheckMemberSupplies(member))
        {
            Debug.Log("Sufficient supplies for " + member.memberName);
        }
        else
        {
            Debug.Log("Insufficient supplies for " + member.memberName);
        }
    }
    /// <summary>
    /// Check the supplies from the family supply.
    /// </summary>
    public bool CheckMemberSupplies(FamilyMemberBehaviour member)
    {
        if (Settings.numOfFoodItems > 0 || Settings.numOfWaterItems > 0)
        {
            Debug.Log("Sufficient supplies for " + member.memberName);
            return true;
        }
        else
        {
            Debug.Log("Insufficient supplies for " + member.memberName);
            return false;
        }
    }
    /// <summary>
    /// Update the dropdown options for family members.
    /// </summary>
    public void UpdateDropdownOptions()
    {
        foreach (TMP_Dropdown dropdown in memberDropdowns)
        {
            dropdown.ClearOptions();
            List<string> options = new List<string>();
            foreach (FamilyMemberBehaviour member in familyMembers)
            {
                options.Add(member.memberName);
            }
            dropdown.AddOptions(options);
        }
    }
    /// <summary>
    /// Fill the family members list from the family manager.
    /// </summary>
    public void FillFamilyMembersList()
    {
        familyMembers = familyManager.familyMembers;
    }
}
