using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableOBJ : MonoBehaviour
{
    public enum InteractionType { None, Pickup, Examine, Activate }
    [Header("Interaction Settings")]
    public InteractionType interactionType = InteractionType.None;
    public string interactionPrompt = "Interact";
    public enum PickupType { None, Key, Health, Food }
    public PickupType pickupType = PickupType.None;
    public bool isCollected = false;
    [Header("UI Elements")]
    public GameObject interactionPromptUI;
    public TextMeshProUGUI interactionPromptText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractionPrompt();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractionPrompt();
        }
    }
    public void ShowInteractionPrompt()
    {
        interactionPromptText.text = $"Press {interactionPrompt} to interact";
        interactionPromptUI.SetActive(true);
    }
    public void HideInteractionPrompt()
    {
        interactionPromptUI.SetActive(false);
    }
    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Pickup:
                if (!isCollected)
                {
                    CollectItem();
                }
                break;
            case InteractionType.Examine:
                ExamineItem();
                break;
            case InteractionType.Activate:
                ActivateItem();
                break;
            default:
                Debug.Log("No interaction type set.");
                break;
        }
    }
    void CollectItem()
    {
        switch (pickupType)
        {
            case PickupType.Key:
                Debug.Log("Picked up a key.");
                break;
            case PickupType.Health:
                Debug.Log("Picked up health.");
                break;
            case PickupType.Food:
                Debug.Log("Picked up food.");
                break;
            default:
                Debug.Log("Picked up an item.");
                break;
        }
    }
    void ExamineItem()
    {
        Debug.Log("Examining item...");
    }
    void ActivateItem()
    {
        Debug.Log("Activating item...");
    }



}
