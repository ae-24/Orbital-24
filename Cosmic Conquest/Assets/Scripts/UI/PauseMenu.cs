using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [SerializeField] GameSession gameSession;
    public static bool isPaused;

    private int currentSceneIndex;
    
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
        isPaused = false;
        Time.timeScale = 1f;
        HighScoreManager.Instance.CheckHighScore(gameSession.score);
        PlayerPrefs.SetInt("playerStarted", 1);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        PlayerPrefs.SetInt("PlayerLives", FindObjectOfType<GameSession>().playerLives);
        PlayerPrefs.SetInt("ScoreValue", FindObjectOfType<GameSession>().score);
        
        PlayerPos playerPos = FindObjectOfType<PlayerPos>();
        if (playerPos != null)
        {
            playerPos.SavePos();
        }
        else
        {
            Debug.LogError("PlayerPos not found!");
        }
        
        PlayerPrefs.Save();
        FindObjectOfType<GameSession>().scoreText.enabled = false;
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void Quit() 
    {
        Application.Quit();
        Application.OpenURL("about:blank");
    }
}
