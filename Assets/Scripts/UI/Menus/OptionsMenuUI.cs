using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuUI : MonoBehaviour
{

    [SerializeField] private GameObject optionMenuCanvas;

    public void OpenOptionsMenu()
    {
        optionMenuCanvas.SetActive(true);
        InputHandler.Instance.SwitchToUI();
    }

    public void ReturnToGame()
    {
        optionMenuCanvas.SetActive(false);
        if (!SceneHelper.IsCurrentScene("DockHub")) InputHandler.Instance.SwitchToGameplay();
    }

    public void HowToPlay()
    {

    }

    public void ToSettings()
    {

    }

    public void ToMainMenu()
    {
        SceneHelper.GoToMainMenu();
    }

    public void ToDockHub()
    {
        SceneHelper.GoToDockHub();
    }

    private void Awake()
    {
        optionMenuCanvas.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
