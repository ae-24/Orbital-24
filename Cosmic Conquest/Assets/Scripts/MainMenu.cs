using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
