using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Loading Game Scene");
        SceneManager.LoadScene("WessonScene2");
    }


    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();  
    }
}