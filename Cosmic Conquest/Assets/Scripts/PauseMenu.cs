using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] GameSession gameSession;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused) 
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }   
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
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
