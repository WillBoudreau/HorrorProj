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
    public string interactionPrompt = "E";
    public enum PickupType { None, Key, Health, Food }
    public PickupType pickupType = PickupType.None;
    public bool isCollected = false;
    [Header("UI Elements")]
    public GameObject interactionPromptUI;
    public TextMeshProUGUI interactionPromptText;
    [Header("Highlight Settings")]
    public GameObject highlightEffect;
    public Color highlightColor = Color.yellow;
    public Material highlightMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractionPrompt();
            AddHighlight();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractionPrompt();
            StartCoroutine(RevertHighlight(GetComponent<Renderer>(), GetComponent<Renderer>().material, 0.1f));
        }
    }
    public void ShowInteractionPrompt()
    {
        interactionPromptText.text = $"{interactionPrompt}";
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
    /// <summary>
    /// Add a highlight effect to the object when the player looks at it.
    /// </summary>
    public void AddHighlight()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            highlightEffect.SetActive(true);
            highlightMaterial.color = highlightColor;
        }
    }
    IEnumerator RevertHighlight(Renderer renderer, Material originalMaterial, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (renderer != null)
        {
            renderer.material = originalMaterial;
            highlightEffect.SetActive(false);
        }
    }

}
