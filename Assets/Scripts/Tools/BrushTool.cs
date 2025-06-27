using UnityEngine;

public class BrushTool
{
    public float MaxDurability { get; private set; }
    public float CurrentDurability { get; private set; }

    public BrushTool(float maxDurability, float currentDurability)
    {
        MaxDurability = maxDurability;
        CurrentDurability = currentDurability;
    }

    public void ResetDurability()
    {
        CurrentDurability = MaxDurability;
    }

    public void DecreaseDurability(float amount)
    {
        CurrentDurability = Mathf.Max(CurrentDurability - amount, 0f);
    }

    public float GetDurabilityPercent()
    {
        return MaxDurability <= 0f ? 0f : CurrentDurability / MaxDurability;
    }

    public bool IsBroken => CurrentDurability <= 0f;
}
