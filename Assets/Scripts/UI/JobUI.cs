using TMPro;
using UnityEngine;

public class JobUI : MonoBehaviour
{

    public static JobUI Instance;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI basicBarnacleText;
    public TextMeshProUGUI smallBarnacleText;
    public TextMeshProUGUI brushDurabilityText;

    public int basicBarnacleCount { get; private set; } = 0;
    public int smallBarnacleCount { get; private set; } = 0;

    void Awake()
    {
        Instance = this;
    }

    public void SetTimer(int minutes, int seconds)
    {
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncrementBasicBarnacleCount()
    {
        basicBarnacleCount++;
        UpdateText();
    }

    public void IncrementSmallBarnacleCount()
    {
        smallBarnacleCount++;
        UpdateText();
    }

    void UpdateText()
    {
        basicBarnacleText.text = $"Basic: {basicBarnacleCount}";
        smallBarnacleText.text = $"Small: {smallBarnacleCount}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float percent = ToolManager.Instance.brushTool.GetDurabilityPercent() * 100f;
        brushDurabilityText.text = string.Format("{0:0}%", percent);
    }
}
