using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private GameSession gameSession;
    public static bool isPaused;

    private int currentSceneIndex;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null)
        {
            Debug.LogError("GameSession not found!");
            return;
        }

        gameSession.DisableScoreText();

        isPaused = false;
        HighScoreManager.Instance.CheckHighScore(gameSession.score);

        PlayerPrefs.SetInt("playerStarted", 1);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        PlayerPrefs.SetInt("PlayerLives", gameSession.playerLives);
        PlayerPrefs.SetInt("ScoreValue", gameSession.score);

        PlayerPos playerPos = FindObjectOfType<PlayerPos>();
        if (playerPos != null)
        {
            playerPos.SavePos();
        }
        else
        {
            Debug.LogError("PlayerPos not found!");
        }

        EnemyManager.Instance.SaveAllEnemyPositions();

        PlayerPrefs.Save();

        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSecondsRealtime(1f);

        DestroyPersistentObjects();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    private void DestroyPersistentObjects()
    {
        if (LimitedVisionManager.Instance != null)
        {
            Destroy(LimitedVisionManager.Instance.vignetteImage);
            Destroy(LimitedVisionManager.Instance.gameObject);
        }

        if (EnemyManager.Instance != null)
        {
            Destroy(EnemyManager.Instance.gameObject);
        }

        // No need to destroy CollectibleManager here
    }
}
