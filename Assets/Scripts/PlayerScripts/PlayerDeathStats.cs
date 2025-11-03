using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerDeathStats : MonoBehaviour
{
    [Header("Death Stats UI")]
    public TextMeshProUGUI deathMessageText;
    public TextMeshProUGUI causeOfDeathText;
    public List<string> deathMessages = new List<string>
    {
        "You have met a tragic end.",
        "Your journey has come to an untimely conclusion.",
        "Fate has not been kind to you.",
        "You fought bravely, but alas, it was not enough.",
        "The darkness has claimed you."
    };
    public List<string> deathCauses = new List<string>
    {
        "Health Depleted",
        "Starvation",
        "Dehydration",
        "Injury",
        "Unknown Forces"
    };
    /// <summary>
    /// Set the death message based on the cause of death.
    /// </summary>
    /// <param name="cause">The cause of death.</param>
    public void SetDeathMessage(string cause)
    {
        Debug.Log("Setting death message for cause: " + cause);
        int causeIndex = deathCauses.IndexOf(cause);
        if (causeIndex != -1 && causeIndex < deathMessages.Count)
        {
            deathMessageText.text = deathMessages[causeIndex];
            causeOfDeathText.text = "You died from: " + cause;
        }
        else
        {
            deathMessageText.text = "Your demise is a mystery.";
        }
    }

}
