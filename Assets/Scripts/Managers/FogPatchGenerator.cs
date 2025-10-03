using UnityEngine;


public class FogPatchGenerator : MonoBehaviour
{
    [Header("Fog Patch settings")]
    [SerializeField] private GameObject fogPatchPrefab;
    [SerializeField] private int fogPatchCount = 5;
    [SerializeField] private float fogPatchRadius = 10f;
    [SerializeField] private float fogPatchHeight = 5f;
    [SerializeField] private float fogPatchScaleMin = 1f;
    [SerializeField] private float fogPatchScaleMax = 3f;
    [SerializeField] private GameObject[] FogPatchParents;
    private GameObject[] fogPatches;

    void Start()
    {
        foreach (var parent in FogPatchParents)
        {
            GenerateFogPatches(parent.transform.position, fogPatchCount, fogPatchRadius, fogPatchHeight, fogPatchScaleMin, fogPatchScaleMax);
        }
    }
    /// <summary>
    /// Generates fog patches around the map.
    /// </summary>
    /// <param name="center"></param>
    /// <param name="count"></param>
    /// <param name="radius"></param>
    /// <param name="height"></param>
    /// <param name="scaleMin"></param>
    /// <param name="scaleMax"></param>
    public void GenerateFogPatches(Vector3 center, int count, float radius, float height, float scaleMin, float scaleMax)
    {
        if (fogPatches != null)
        {
            foreach (var patch in fogPatches)
            {
                Destroy(patch);
            }
        }

        fogPatches = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * radius;
            Vector3 spawnPos = new Vector3(center.x + randomPos.x, center.y + height, center.z + randomPos.y);
            GameObject fogPatch = Instantiate(fogPatchPrefab, spawnPos, Quaternion.Euler(90f, 0f, 0f));
            float randomScale = Random.Range(scaleMin, scaleMax);
            fogPatch.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            fogPatches[i] = fogPatch;
        }
    }
}
