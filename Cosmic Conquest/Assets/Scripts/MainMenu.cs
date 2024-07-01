using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // private int sceneToContinue;
    public void PlayGame()
    {
        // sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        
        // if(sceneToContinue != 0) 
        // {
        //     SceneManager.LoadSceneAsync(sceneToContinue);
        // }
        // else 
        // {
            SceneManager.LoadSceneAsync(1);
            FindObjectOfType<GameSession>().playerLives = 3;
            FindObjectOfType<GameSession>().score = 0;
            FindObjectOfType<GameSession>().scoreText.enabled = true;
            FindObjectOfType<GameSession>().scoreText.text = FindObjectOfType<GameSession>().score.ToString();
        // }
        
    }

    public void Quit() 
    {
//if (UNITY_EDITOR || DEVELOPMENT_BUILD)
    Debug.Log(this.name+" : "+this.GetType()+" : "+System.Reflection.MethodBase.GetCurrentMethod().Name); 
//endif
//if (UNITY_EDITOR)
    UnityEditor.EditorApplication.isPlaying = false;
//elif (UNITY_STANDALONE) 
    Application.Quit();
//elif (UNITY_WEBGL)
    Application.OpenURL("about:blank");
//endif
  }
}
