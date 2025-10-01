using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogNoiseGenerator : MonoBehaviour
{
    [Header("Fog settings")]
    [SerializeField] private Material fogMaterial;
    [SerializeField] private float noiseScale = 0.1f;
    [SerializeField] private float noiseSpeed = 1f;
    private float noiseOffset = 0f;

    void Start()
    {
        // Enable Unity's built-in fog
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = Color.gray;
        RenderSettings.fogDensity = 0.02f;
    }

    void Update()
    {
        GenerateRandomNoiseOffset();
        UpdateFogMaterial(noiseScale, noiseSpeed, noiseOffset);
    }

    /// <summary>
    /// Generates a random noise offset for the fog effect.
    /// </summary>
    public void GenerateRandomNoiseOffset()
    {
        noiseOffset = Random.Range(0f, 100f);
        fogMaterial.SetFloat("_NoiseOffset", noiseOffset);
    }

    /// <summary>
    /// Updates the fog material with noise parameters.
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="speed"></param>
    /// <param name="offset"></param>
    public void UpdateFogMaterial(float scale, float speed, float offset)
    {
        fogMaterial.SetFloat("_NoiseScale", scale);
        fogMaterial.SetFloat("_NoiseSpeed", speed);
        fogMaterial.SetFloat("_NoiseOffset", offset);
    }
}