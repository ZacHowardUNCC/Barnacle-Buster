using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class JobResultUI : MonoBehaviour
{
    public static JobResultUI Instance;

    private CanvasGroup canvasGroup;
    [SerializeField] private Image backgroundImage;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button resultsButton;
    [SerializeField] private Button retryJobButton;
    [SerializeField] private Button dockHubButton;
    [Header("Results Group Elements")]
    [SerializeField] private GameObject jobResultsGroup;
    [SerializeField] private TextMeshProUGUI totalBarnaclesClearedText;
    [SerializeField] private TextMeshProUGUI basicBarnaclesClearedText;
    [SerializeField] private TextMeshProUGUI smallBarnaclesClearedText;
    [SerializeField] private TextMeshProUGUI coinsEarnedText;
    [Header("Other Elements")]
    [SerializeField] private GameObject optionMenuCanvas;
    [SerializeField] private Button settingsButton;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        resultsButton.gameObject.SetActive(false);
        retryJobButton.gameObject.SetActive(false);
        dockHubButton.gameObject.SetActive(false);

        jobResultsGroup.gameObject.SetActive(false);
    }

    public void ShowResult(bool passed)
    {
        optionMenuCanvas.SetActive(false);
        InputHandler.Instance.SwitchToUI();
        settingsButton.interactable = false;
        // Set the correct UI state
        resultText.text = passed ? "Job Passed!" : "Job Failed.";

        if (passed)
        {
            StartCoroutine(EnableResultsButtonDelay(1f)); // delay results button interaction
        }

        retryJobButton.gameObject.SetActive(!passed);
        dockHubButton.gameObject.SetActive(!passed);

        StartCoroutine(FadeInOverlay());
    }

    private IEnumerator FadeInOverlay()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Fade in background to 232 alpha (out of 255)
            float bgAlpha = Mathf.Lerp(0f, 232f / 255f, t);
            backgroundImage.color = new Color(0f, 0f, 0f, bgAlpha);

            // Fade in entire UI canvas
            canvasGroup.alpha = Mathf.Clamp01(t);

            yield return null;
        }

        // Ensure final state
        backgroundImage.color = new Color(0f, 0f, 0f, 232f / 255f);
        canvasGroup.alpha = 1f;
    }

    private IEnumerator EnableResultsButtonDelay(float delay)
    {
        resultsButton.interactable = false;
        resultsButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        resultsButton.interactable = true;
    }

    public void ViewResults()
    {
        var (total, basic, small, coins) = JobManager.Instance.GetJobStats();

        GameState.Instance.Coins += coins;

        resultText.gameObject.SetActive(false);
        resultsButton.gameObject.SetActive(false);
        jobResultsGroup.gameObject.SetActive(true);

        totalBarnaclesClearedText.text = total.ToString();
        basicBarnaclesClearedText.text = basic.ToString();
        smallBarnaclesClearedText.text = small.ToString();
        coinsEarnedText.text = coins.ToString();
    }

    public void RetryJob()
    {
        InputHandler.Instance.SwitchToGameplay();
        SceneHelper.ReloadCurrentScene();
    }

    public void ToDockHub()
    {
        InputHandler.Instance.SwitchToUI();
        GameState.Instance.BrushDurability = ToolManager.Instance.brushTool.CurrentDurability;
        SceneHelper.GoToDockHub();
    }
}
