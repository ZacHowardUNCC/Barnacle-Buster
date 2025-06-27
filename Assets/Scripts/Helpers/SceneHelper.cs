using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper
{
    public static void GoToSaveFiles()
    {
        SceneManager.LoadScene("SaveFiles");
    }

    public static void GoToDockHub()
    {
        InputHandler.Instance.SwitchToUI();
        SceneManager.LoadScene("DockHub");
    }

    public static void GoToShop()
    {
        InputHandler.Instance.SwitchToUI();
        SceneManager.LoadScene("Shop");
    }

    public static void GoToMainMenu()
    {
        InputHandler.Instance.SwitchToUI();
        SceneManager.LoadScene("MainMenu");
    }

    public static void GoToTurtleJob()
    {
        InputHandler.Instance.SwitchToGameplay();
        SceneManager.LoadScene("TurtleJob");
    }

    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static bool IsCurrentScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
}
