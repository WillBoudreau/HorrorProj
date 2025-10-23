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
        loadingBar.fillAmount = progress;
    }
    private void SetLoadingText(string text)
    {
        loadingText.text = text;
    }

    public IEnumerator StartLoadingSequence()
    {
        SetLoadingText("Loading...");
        isLoading = true;
        yield return StartCoroutine(FadeIn());
        for (float progress = 0; progress <= 1; progress += 0.1f)
        {
            LoadingBarProgress(progress);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut());
    }
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
