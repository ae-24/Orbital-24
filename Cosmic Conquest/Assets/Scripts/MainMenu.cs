using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int sceneToContinue;

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
            SceneManager.LoadSceneAsync(sceneToContinue);

            StartCoroutine(LoadGameSession());
        }
        else
        {
            Debug.LogError("No saved game found.");
        }
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
            if (gameSession.scoreText != null)
            {
                gameSession.scoreText.enabled = true;
                gameSession.scoreText.text = gameSession.score.ToString();
            }

            // Wait until the PlayerPos object is available in the scene
            PlayerPos playerPos = null;
            int attempts = 0;
            while (playerPos == null && attempts < 10) // Limit the number of attempts to avoid an infinite loop
            {
                yield return new WaitForSeconds(0.1f); // Wait for a short time before trying again
                playerPos = FindObjectOfType<PlayerPos>();
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
