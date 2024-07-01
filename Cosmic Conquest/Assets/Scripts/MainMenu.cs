using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
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
