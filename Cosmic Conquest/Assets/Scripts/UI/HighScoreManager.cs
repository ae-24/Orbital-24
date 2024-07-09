using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    public TextMeshProUGUI highScoreText;

    private int highScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("High Score: " + highScore);
        DisplayHighScore();
    }

    public void CheckHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            DisplayHighScore();
        }
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        DisplayHighScore();
    }

    public void DisplayHighScore()
    {
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
        else
        {
            Debug.LogWarning("HighScoreText is not assigned in the inspector.");
        }
    }
}
