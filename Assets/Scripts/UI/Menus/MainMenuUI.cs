using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneHelper.GoToSaveFiles();
    }

    public void QuitGame()
    {
        Debug.Log("Closing Game...");
        Application.Quit();
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
