using UnityEngine;

public class ToolManager : MonoBehaviour
{

    public static ToolManager Instance;
    public enum ToolType { Pick, Scrapper, Grinder, WireBrush }

    private ToolType currentTool;
    public ToolType CurrentTool => currentTool;
    // Brush variables
    public BrushTool brushTool { get; private set; }
    [SerializeField] private float maxBrushDurability = 100f;
    [SerializeField] private float brushDurabilityLossPerSecond = 1f;
    // Short hand functions, getter for first one, tool type checks for the next 2
    public float BrushDurabilityLossPerSecond => brushDurabilityLossPerSecond;
    public bool IsPickTool() => currentTool == ToolType.Pick;
    public bool IsBrushTool() => currentTool == ToolType.WireBrush;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentTool = ToolType.Pick;

        if (!GameState.Instance.ToolStatsInitialized)
        {
            GameState.Instance.BrushDurability = maxBrushDurability;
            GameState.Instance.MaxBrushDurability = maxBrushDurability;
            GameState.Instance.ToolStatsInitialized = true;
            GameState.Instance.GrantShopAccess();
        }

        brushTool = new BrushTool(GameState.Instance.MaxBrushDurability, GameState.Instance.BrushDurability);
        Debug.Log("Max dur: " + maxBrushDurability);
        Debug.Log("Loss per sec: " + brushDurabilityLossPerSecond);
        Debug.Log(brushTool.CurrentDurability);
    }

    public void SetTool(ToolType newTool)
    {
        currentTool = newTool;
        ToolCursor.Instance.SetSprite(0); // updates cursor to idle sprite immediately
    }

    public void SwapTool() 
    {
        if (currentTool == ToolType.Pick)
        {
            currentTool = ToolType.WireBrush;
        }
        else
        {
            currentTool = ToolType.Pick;
        }

        //Debug.Log("Tool has Swapped");
        ToolCursor.Instance.SwapDefaultSprite();
    }

    public void AnimateTool(int spriteFrame)
    {
        ToolCursor.Instance.SetSprite(spriteFrame);
    }
}
