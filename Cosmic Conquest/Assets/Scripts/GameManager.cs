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
        PlayerPrefs.DeleteAll();
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
            if (gameSession.scoreText != null)
            {
                gameSession.scoreText.enabled = true;
                gameSession.scoreText.text = gameSession.score.ToString();
            }
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
            Debug.Log("Player Lives and Score Set.");
            if (gameSession.scoreText != null)
            {
                gameSession.scoreText.enabled = true;
                gameSession.scoreText.text = gameSession.score.ToString();
            }
            Debug.Log("Player Score Linked.");

            // Wait until the PlayerPos object is available in the scene
            PlayerPos playerPos = null;
            int attempts = 0;
            Debug.Log("playerPos and attempts initiated.");
            while (playerPos == null && attempts < 20) // Increase attempts count
            {
                Debug.Log("In while loop.");
                yield return new WaitForSeconds(0.1f); // Increase wait time
                Debug.Log("waiting...");
                playerPos = FindObjectOfType<PlayerPos>();
                Debug.Log("set playerPos");
                attempts++;
                Debug.Log("Attempting to find PlayerPos: attempt " + attempts);
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

    public void Quit()
    {
        Application.Quit();
        Application.OpenURL("about:blank");
    }
}
