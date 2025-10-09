using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class AdjustSettings : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Video Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Player Settings")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Toggle invertYToggle;

    [Header("Gameplay Settings")]
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private Toggle tutorialToggle;

    private Resolution[] availableResolutions;

    void Start()
    {
        // Initialize UI elements with current settings
        masterVolumeSlider.value = Settings.masterVolume;
        musicVolumeSlider.value = Settings.musicVolume;
        sfxVolumeSlider.value = Settings.sfxVolume;

        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));
            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.value = Settings.screenResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Settings.isFullScreen;

        sensitivitySlider.value = Settings.playerSensitivity;
        invertYToggle.isOn = Settings.invertYAxis;

        difficultySlider.value = Settings.difficultyLevel;
        tutorialToggle.isOn = Settings.isTutorialEnabled;
    }

    public void OnMasterVolumeChanged(float value)
    {
        Settings.masterVolume = value;
        Settings.ApplySettings();
    }

    public void OnMusicVolumeChanged(float value)
    {
        Settings.musicVolume = value;
        // Apply music volume changes if needed
    }

    public void OnSFXVolumeChanged(float value)
    {
        Settings.sfxVolume = value;
        // Apply SFX volume changes if needed
    }

    public void OnResolutionChanged(int index)
    {
        Settings.screenResolutionIndex = index;
        Resolution res = availableResolutions[index];
        Screen.SetResolution(res.width, res.height, Settings.isFullScreen);
    }
    public void OnYToggleChanged()
    {
        if (invertYToggle.isOn)
        {
            Settings.invertYAxis = true;
            Debug.Log("Inverted Y Axis set to true");
        }
        else
        {
            Settings.invertYAxis = false;
            Debug.Log("Inverted Y Axis set to false");
        }
    }
    public void OnSensitivityChanged(float value)
    {
        Settings.playerSensitivity = value;
    }
}