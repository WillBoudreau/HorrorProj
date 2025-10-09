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