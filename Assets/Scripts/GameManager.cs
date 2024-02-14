using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int nextLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadLevel()
    {
        nextLevel = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        Debug.Log(nextLevel);
        if (nextLevel != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            LoadLevel();
        } 
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