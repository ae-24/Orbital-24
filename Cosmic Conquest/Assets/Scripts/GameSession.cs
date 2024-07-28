using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    [SerializeField] public int score = 0;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        if (highScoreText != null)
        {
            HighScoreManager.Instance.SetHighScoreText(highScoreText);
        }
    }

    public void EnableScoreText()
    {
        if (scoreText != null)
        {
            scoreText.enabled = true;
            scoreText.text = score.ToString();
        }
    }

    public void DisableScoreText()
    {
        if (scoreText != null)
        {
            scoreText.enabled = false;
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            playerLives = 0;
            if (HighScoreManager.Instance != null)
            {
                HighScoreManager.Instance.CheckHighScore(score);
            }

            ResetGameSession();
        }
    }

    public void UpdateScore(int value)
    {
        score = value;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    void TakeLife()
    {
        playerLives = Mathf.Max(0, playerLives - 1); // Ensure lives do not go below zero
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddHealth()
    {
        if (playerLives < 3)
        {
            playerLives += 1;
        }
    }

    public void ResetGameSession()
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.CheckHighScore(score);
        }

        if (FindObjectOfType<ScenePersist>() != null)
        {
            FindObjectOfType<ScenePersist>().ResetScenePersist();
        }

        Collectible.ResetAllCollectibles();
        EnemyState.ResetAllEnemies();
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("ScoreValue");

        // Resetting player lives and score
        playerLives = 3;
        score = 0;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 1)
        {
            SceneManager.LoadScene(currentSceneIndex - 1); // Assumes your first level is build index 1
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        Destroy(gameObject);
    }
}
