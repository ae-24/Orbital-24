using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private int sceneToContinue;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        FindObjectOfType<GameSession>().playerLives = 3;
        FindObjectOfType<GameSession>().score = 0;
        FindObjectOfType<GameSession>().scoreText.enabled = true;
        FindObjectOfType<GameSession>().scoreText.text = FindObjectOfType<GameSession>().score.ToString();
        
        
    }

    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        
            if(sceneToContinue != 0) 
            {
            SceneManager.LoadSceneAsync(sceneToContinue);
            }
            
    }

    public void Quit() 
    {
    Application.Quit();
    Application.OpenURL("about:blank");
    }
}
