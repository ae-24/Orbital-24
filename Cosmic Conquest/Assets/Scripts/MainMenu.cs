using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        HighScoreManager.Instance.highScoreText = highScoreText;
        HighScoreManager.Instance.DisplayHighScore();
    }

    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    public void ResetHighScore()
    {
        HighScoreManager.Instance.ResetHighScore();
    }
}
