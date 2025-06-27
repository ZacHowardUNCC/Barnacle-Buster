using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI brushMaxDurabilityText;
    [SerializeField] private TextMeshProUGUI brushCurrentDurabilityText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI repairCostText;

    [SerializeField] private int upgradeCost = 200;
    [SerializeField] private int repairCost = 150;

    public void ToDockHub()
    {
        InputHandler.Instance.SwitchToUI();
        SceneHelper.GoToDockHub();
    }

    void Awake()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        coinsText.text = GameState.Instance.Coins.ToString();

        float max = GameState.Instance.MaxBrushDurability;
        float current = GameState.Instance.BrushDurability;

        float percent = (max == 0f) ? 0f : (current / max) * 100f;

        brushMaxDurabilityText.text = $"Durability: {max}";
        brushCurrentDurabilityText.text = $"{percent:0}%";

        upgradeCostText.text = $"Cost: {upgradeCost} coins";
        repairCostText.text = $"Cost: {repairCost} coins";
        Debug.Log(GameState.Instance.BrushDurability);
    }

    public void UpgradeBrushTool()
    {
        if (GameState.Instance.Coins >= upgradeCost)
        {
            GameState.Instance.Coins -= upgradeCost;

            // Increase max durability by 10
            GameState.Instance.MaxBrushDurability += 10f;

            // refresh UI values
            RefreshUI();
        }
        else
        {
            Debug.Log("Not enough coins to upgrade!");
        }
    }

    public void RepairBrushTool()
    {
        if (GameState.Instance.Coins >= repairCost)
        {
            GameState.Instance.Coins -= repairCost;
            float current = GameState.Instance.MaxBrushDurability;
            GameState.Instance.BrushDurability = current;

            RefreshUI();
        }
        else
        {
            Debug.Log("Not enough coins to repair!");
        }
    }
}
