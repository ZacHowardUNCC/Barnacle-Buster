using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    // Private game state variables
    private int coins = 0;
    private float brushDurability = 100f;
    private float maxBrushDurability = 100f;
    private float gameTime = 0f;

    public bool canAccessShop { get; private set; } = false;
    public void GrantShopAccess() => canAccessShop = true;

    private bool gameRunning = false;
    public bool GameRunning
    {
        get => gameRunning;
        set => gameRunning = value;
    }

    private bool toolStatsInitialized = false;
    public bool ToolStatsInitialized
    {
        get => toolStatsInitialized;
        set => toolStatsInitialized = value;
    }

    // Public getters and setters for the private variables
    public int Coins
    {
        get => coins;
        set => coins = Mathf.Max(0, value); // clamp to prevent negatives
    }

    public float BrushDurability
    {
        get => brushDurability;
        set => brushDurability = Mathf.Clamp(value, 0f, MaxBrushDurability);
    }

    public float MaxBrushDurability
    {
        get => maxBrushDurability;
        set => maxBrushDurability = Mathf.Max(1f, value); // minimum of 1
    }

    public float GameTime => gameTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!gameRunning) return;
        gameTime += Time.deltaTime;
    }
}
