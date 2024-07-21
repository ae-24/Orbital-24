using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] public volatile int playerLives = 3; // Number of player lives
    [SerializeField] public volatile int score = 0;

    [SerializeField] public TextMeshProUGUI scoreText;

    void Awake()
    {
        // Ensures that there is at most one game session running to keep track of player data
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
        scoreText.text = score.ToString();
    }

    // Resets game session when player runs out of lives
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            HighScoreManager.Instance.CheckHighScore(score);
            ResetGameSession();
        }
    }

    public void UpdateScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    // Reduces player lives count when player dies, and resets them to the start of their current level
    void TakeLife()
    {
        playerLives -= 1;
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

    // Destroys current game session and reinitializes to the first level
    void ResetGameSession()
    {
        HighScoreManager.Instance.CheckHighScore(score);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Collectible.ResetAllCollectibles(); // Reset all collectibles
        EnemyState.ResetAllEnemies(); // Reset enemy IDs
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("ScoreValue");
        SceneManager.LoadSceneAsync(1);
        Destroy(gameObject);
    }
}
