using UnityEngine;

public class SaveFilesUI : MonoBehaviour
{
    public void SaveFile1()
    {
        SceneHelper.GoToDockHub();
        GameState.Instance.GameRunning = true;
    }

    public void SaveFile2()
    {
        SceneHelper.GoToDockHub();
        GameState.Instance.GameRunning = true;
    }

    public void SaveFile3()
    {
        SceneHelper.GoToDockHub();
        GameState.Instance.GameRunning = true;
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
