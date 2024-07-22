using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        Debug.Log("PlayGame button pressed.");

        // Delete specific PlayerPrefs keys related to the game session
        PlayerPrefs.DeleteKey("playerStarted");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("ScoreValue");

        Collectible.ResetAllCollectibles(); // Reset all collectibles
        EnemyState.ResetAllEnemies(); // Reset enemy IDs

        // Optionally, reset other game-related data here

        SceneManager.LoadScene(1);

        StartCoroutine(InitializeGameSession());
    }

    private IEnumerator InitializeGameSession()
    {
        yield return null; // Wait for the next frame to ensure the scene is loaded

        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.playerLives = 3;
            gameSession.score = 0;
            gameSession.EnableScoreText();
        }
        else
        {
            Debug.LogError("GameSession not found!");
        }
    }

    public void ContinueGame()
    {
        Debug.Log("ContinueGame button pressed.");
        if (PlayerPrefs.HasKey("playerStarted"))
        {
            sceneToContinue = PlayerPrefs.GetInt("SavedScene");
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadSceneAsync(sceneToContinue);
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
        Debug.Log("Starting LoadGameSession coroutine.");
        yield return null; // Wait for the next frame to ensure the scene is loaded

        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            Debug.Log("GameSession found.");
            gameSession.playerLives = PlayerPrefs.GetInt("PlayerLives");
            gameSession.score = PlayerPrefs.GetInt("ScoreValue");
            gameSession.EnableScoreText();

            // Wait until the PlayerPos object is available in the scene
            PlayerPos playerPos = null;
            Debug.Log("Looking for PlayerPos.");
            while (playerPos == null)
            {
                yield return null; // Wait for the next frame to ensure all objects are initialized
                playerPos = FindObjectOfType<PlayerPos>();
            }

            if (playerPos != null)
            {
                Debug.Log("PlayerPos found, loading position...");
                playerPos.LoadPos();
            }
            else
            {
                Debug.LogError("PlayerPos not found after multiple attempts.");
            }
        }
        else
        {
            Debug.LogError("GameSession not found!");
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("playerStarted", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("PlayerLives", FindObjectOfType<GameSession>().playerLives);
        PlayerPrefs.SetInt("ScoreValue", FindObjectOfType<GameSession>().score);
        FindObjectOfType<PlayerPos>().SavePos();
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        Application.Quit();
        Application.OpenURL("about:blank");
    }
}
