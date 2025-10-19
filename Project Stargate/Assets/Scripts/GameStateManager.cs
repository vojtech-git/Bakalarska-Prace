using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public String firstScene;
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        MapRuntime.I.StartAtCenter();
    }
}
