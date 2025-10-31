using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class LoadingScreenBehaviour : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Image loadingBar;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float displayDuration = 0.5f;
    [SerializeField] private float loadingScreenDuration = 2f;
    public bool isLoading = false;

    private void LoadingBarProgress(float progress)
    {
        loadingBar.transform.localScale = new Vector3(progress, 1, 1);
        Debug.Log("Loading progress: " + (progress * 100f) + "%");
    }
    private void SetLoadingText(string text)
    {
        loadingText.text = text;
    }


    /// <summary>
    /// Starts the loading screen sequence.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartLoadingSequence()
    {
        SetLoadingText("Loading...");
        isLoading = true;
        yield return StartCoroutine(FadeIn());
        for (float progress = 0; progress <= 1; progress += 1f)
        {
            yield return new WaitForSeconds(0.2f);
            LoadingBarProgress(progress);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut());
    }
    /// <summary>
    /// Fades in the loading screen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        Debug.Log("Fading in loading screen.");
        float elapsed = 0f;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeInDuration);
            yield return null;
        }
    }
    /// <summary>
    /// Fades out the loading screen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsed / fadeOutDuration);
            yield return null;
        }
        isLoading = false;
    }
}
