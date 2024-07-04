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
        PlayerPrefs.DeleteAll();
        FindObjectOfType<GameSession>().playerLives = 3;
        FindObjectOfType<GameSession>().score = 0;
        FindObjectOfType<GameSession>().scoreText.enabled = true;
        FindObjectOfType<GameSession>().scoreText.text = FindObjectOfType<GameSession>().score.ToString();
        //FindObjectOfType<PlayerMovement>().playerPos.x = (float) -16.48;
        //FindObjectOfType<PlayerMovement>().playerPos.y = (float) -1.44;
    }

    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if(sceneToContinue != 0) 
        {
            SceneManager.LoadSceneAsync(sceneToContinue);
            FindObjectOfType<GameSession>().playerLives = PlayerPrefs.GetInt("PlayerLives");
            FindObjectOfType<GameSession>().score = PlayerPrefs.GetInt("ScoreValue");
            //FindObjectOfType<PlayerMovement>().playerPos.x = PlayerPrefs.GetFloat("PlayerPosX");
            //FindObjectOfType<PlayerMovement>().playerPos.y = PlayerPrefs.GetFloat("PlayerPosY");
            FindObjectOfType<GameSession>().scoreText.enabled = true;
            FindObjectOfType<GameSession>().scoreText.text = FindObjectOfType<GameSession>().score.ToString();
        }
        

        
            
    }

    public void Quit() 
    {
    Application.Quit();
    Application.OpenURL("about:blank");
    }
}
