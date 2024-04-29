using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionManager : MonoBehaviour
{
    private int nextLevel;
    public void LoadLevel()
    {
        nextLevel = Random.Range(2, SceneManager.sceneCountInBuildSettings);
        Debug.Log(nextLevel);
        if (nextLevel != SceneManager.GetActiveScene().buildIndex)
        {
            //SceneManager.LoadScene(nextLevel);
            SceneManager.LoadScene(2);
        }
        else
        {
            LoadLevel();
        } 
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
