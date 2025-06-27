using UnityEngine;

public class JobManager : MonoBehaviour
{

    private bool jobOver = false;
    public static JobManager Instance;

    [SerializeField] private int jobTimerMin = 0;
    [SerializeField] private float jobTimerSec = 30f;

    public int barnaclesCleared { get; private set; } = 0;
    private int smallBarnaclesCleared = 0;
    [SerializeField] int quota = 30;

    [SerializeField] private int coinsPerBasicBarnacle = 5;
    [SerializeField] private int coinsPerSmallBarnacle = 1;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jobOver = false;
        ToolManager.Instance.SetTool(ToolManager.ToolType.Pick);
        JobUI.Instance.SetTimer(jobTimerMin, Mathf.CeilToInt(jobTimerSec));
    }

    // Update is called once per frame
    void Update()
    {
        if (jobOver) return;

        JobUI.Instance.SetTimer(jobTimerMin, Mathf.CeilToInt(jobTimerSec));
        jobTimerSec -= Time.deltaTime;

        if (jobTimerMin == 0 && jobTimerSec <= 0f)
        {
            jobOver = true;
            JobUI.Instance.SetTimer(0, 0);
            JobResult();
            return;
        }

        if (jobTimerSec <= -1f && jobTimerMin > 0)
        {
            jobTimerMin--;
            jobTimerSec += 60f;
        }
    }

    public void BarnacleCleared()
    {
        if (jobOver) return;
        barnaclesCleared++;
    }

    public void SmallBarnacleCleared()
    {
        smallBarnaclesCleared++;

        if (smallBarnaclesCleared >= 12)
        {
            smallBarnaclesCleared -= 12;
            BarnacleCleared();
        }
    }

    public void JobResult()
    {
        //JobUI.Instance.SetTimer(0, 0);

        if (barnaclesCleared >= quota)
        {
            Debug.Log("Job Success!");
            JobResultUI.Instance.ShowResult(true);
        }
        else
        {
            Debug.Log("Job Failed!");
            JobResultUI.Instance.ShowResult(false);
        }

        // Disable spawner or player input if needed
    }

    public (int totalCount, int basicCount, int smallCount, int totalCoins) GetJobStats()
    {
        int total = barnaclesCleared;
        int basic = JobUI.Instance.basicBarnacleCount;
        int small = JobUI.Instance.smallBarnacleCount;
        int coins = (basic * coinsPerBasicBarnacle) + (small * coinsPerSmallBarnacle);
        return (total, basic, small, coins);
    }
}
