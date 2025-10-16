using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private float dayDuration = 120f; // Duration of a full day in seconds
    [SerializeField] private Gradient lightColorGradient; // Gradient for light color over the day
    [SerializeField] private AnimationCurve lightIntensityCurve; // Curve for light intensity over the day
    [SerializeField] private Material skyboxMaterial; // Skybox material to change based on time of day
    [SerializeField] private Material nightSkybox; // Skybox material for night
    private float timeOfDay = 0f; // Current time of day in seconds

    void Update()
    {
        UpdateTimeOfDay();
        UpdateLighting();
    }
    /// <summary>
    /// Updates the time of day based on the day duration.
    /// </summary>
    private void UpdateTimeOfDay()
    {
        timeOfDay += Time.deltaTime;
        if (timeOfDay >= dayDuration)
        {
            timeOfDay = 0f; // Reset to start of day
        }
    }
    /// <summary>
    /// Updates the lighting and skybox based on the current time of day.
    /// </summary>
    private void UpdateLighting()
    {
        float timePercent = timeOfDay / dayDuration;
        if (directionalLight != null)
        {
            directionalLight.color = lightColorGradient.Evaluate(timePercent);
            directionalLight.intensity = lightIntensityCurve.Evaluate(timePercent);
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

        if (skyboxMaterial != null && nightSkybox != null)
        {
            if (timePercent < 0.25f || timePercent > 0.75f) // Night time
            {
                RenderSettings.skybox = nightSkybox;
            }
            else // Day time
            {
                RenderSettings.skybox = skyboxMaterial;
            }
        }
    }
    /// <summary>
    /// Fade outs the skybox to the night skybox or vice versa.
    /// </summary>
    private IEnumerator FadeSkybox()
    {
        float fadeDuration = 2f; // Duration of the fade
        float elapsedTime = 0f;
        Material initialSkybox = RenderSettings.skybox;
        Material targetSkybox = (RenderSettings.skybox == skyboxMaterial) ? nightSkybox : skyboxMaterial;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            RenderSettings.skybox.Lerp(initialSkybox, targetSkybox, t);
            yield return null;
        }
        RenderSettings.skybox = targetSkybox;
    }
}