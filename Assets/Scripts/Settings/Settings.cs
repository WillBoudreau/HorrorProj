using UnityEngine;

public static class Settings 
{
    [Header("Audio Settings")]
    public static float masterVolume { get; set; } = 1.0f;
    public static float musicVolume { get; set; } = 1.0f;
    public static float sfxVolume { get; set; } = 1.0f;

    [Header("Video Settings")]
    public static int screenResolutionIndex { get; set; } = 0;
    public static bool isFullScreen { get; set; } = true;

    [Header("Player Settings")]
    public static float playerSensitivity { get; set; } = 1.0f;
    public static bool invertYAxis { get; set; } = false;
    [Header("Family Settings")]
    public static string memberName { get; set; } = "Unknown";
    public enum HealthStatus { Healthy, Hungry, Sick, Recovering }
    public static HealthStatus healthStatus { get; set; } = HealthStatus.Healthy;
    public static float hungerLevel { get; set; } = 100.0f; // 0 to 100
    public static float thirstLevel { get; set; } = 100.0f; // 0 to 100
    public static float hungerRate { get; set; } = 1.0f; // Rate at which hunger decreases
    public static float thirstRate { get; set; } = 1.5f; // Rate at which thirst decreases
    public static float numOfFoodItems { get; set; } = 0.0f; // Number of food items in inventory
    public static float numOfWaterItems { get; set; } = 0.0f; // Number of water items in inventory
    public static float numOfMedicineItems { get; set; } = 0.0f; // Number of medicine items in inventory
    public static float maxFoodItems { get; set; } = 20.0f;
    public static float maxWaterItems { get; set; } = 20.0f;
    public static float maxMedicineItems { get; set; } = 10.0f;
    [Header("Family Management Settings")]
    public static float foodSupply { get; set; } = 100.0f; // Total food supply for the family
    public static float waterSupply { get; set; } = 100.0f; // Total water supply for the family
    public static float foodConsumptionRate { get; set; } = 1.0f; // Rate at which food is consumed per member
    public static float waterConsumptionRate { get; set; } = 1.5f; // Rate at which water is consumed per member
    public static float dayLengthInSeconds { get; set; } = 120.0f; // Length of a day in seconds
    [Header("Gameplay Settings")]
    public static float difficultyLevel { get; set; } = 1.0f; // 1.0 = Normal, <1.0 = Easy, >1.0 = Hard
    public static bool isTutorialEnabled { get; set; } = true;

    /// <summary>
    /// Resets all settings to their default values.
    /// </summary>
    public static void ResetToDefaults()
    {
        masterVolume = 1.0f;
        musicVolume = 1.0f;
        sfxVolume = 1.0f;

        screenResolutionIndex = 0;
        isFullScreen = true;

        playerSensitivity = 1.0f;
        invertYAxis = false;

        difficultyLevel = 1.0f;
        isTutorialEnabled = true;
    }
    /// <summary>
    /// Applies the current settings to the game.
    /// </summary>
    public static void ApplySettings()
    {
        // Apply audio settings
        AudioListener.volume = masterVolume;

        // Apply video settings
        Screen.fullScreen = isFullScreen;
    }
}