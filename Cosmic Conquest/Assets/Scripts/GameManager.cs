using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI highScoreText;

    private int sceneToContinue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.DeleteKey("playerStarted");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("ScoreValue");

        Collectible.ResetAllCollectibles();
        EnemyState.ResetAllEnemies();

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

        StartCoroutine(InitializeGameSession());
    }

    private IEnumerator InitializeGameSession()
    {
        yield return null;

        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGameSession();
            gameSession.playerLives = 3;
            gameSession.score = 0;
            gameSession.EnableScoreText();
            gameSession.highScoreText = highScoreText;
            HighScoreManager.Instance.SetHighScoreText(highScoreText);
        }
        else
        {
            Debug.LogError("GameSession not found!");
        }
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("playerStarted"))
        {
            sceneToContinue = PlayerPrefs.GetInt("SavedScene");
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadSceneAsync(sceneToContinue);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("No saved game found.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(LoadGameSession());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator LoadGameSession()
    {
        yield return null;

        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.playerLives = PlayerPrefs.GetInt("PlayerLives");
            gameSession.score = PlayerPrefs.GetInt("ScoreValue");
            gameSession.EnableScoreText();
            gameSession.highScoreText = highScoreText;
            HighScoreManager.Instance.SetHighScoreText(highScoreText);

            PlayerPos playerPos = null;
            while (playerPos == null)
            {
                yield return null;
                playerPos = FindObjectOfType<PlayerPos>();
            }

            playerPos?.LoadPos();
            EnemyManager.Instance?.LoadAllEnemyPositions();
        }
        else
        {
            Debug.LogError("GameSession not found!");
        }
    }

    public void SaveGame()
    {
        Debug.Log("SaveGame method called.");
        PlayerPrefs.SetInt("playerStarted", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null)
        {
            Debug.LogError("GameSession not found!");
            return;
        }
        PlayerPrefs.SetInt("PlayerLives", gameSession.playerLives);
        PlayerPrefs.SetInt("ScoreValue", gameSession.score);
        PlayerPos playerPos = FindObjectOfType<PlayerPos>();
        if (playerPos == null)
        {
            Debug.LogError("PlayerPos not found!");
            return;
        }
        playerPos.SavePos();
        PlayerPrefs.Save();
        Debug.Log("SaveGame method completed.");
    }


    public void Quit()
    {
        //Debug.Log("Quit");
        Application.OpenURL("about:blank");
        Application.Quit();
        
    }
}