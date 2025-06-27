using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DockHubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button shopButton;

    void Awake()
    {
        Debug.Log(GameState.Instance.BrushDurability);
        shopButton.interactable = GameState.Instance.canAccessShop;
    }

    public void GoToJobBoard()
    {
        SceneHelper.GoToTurtleJob();
    }

    public void GoToShop()
    {
        SceneHelper.GoToShop();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsText.text = GameState.Instance.Coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
