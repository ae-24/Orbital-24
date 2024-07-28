using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public string enemyID;
    private static bool newGame = false;

    void Start()
    {
        if (newGame)
        {
            PlayerPrefs.DeleteKey(enemyID);
            gameObject.SetActive(true);
        }
        else
        {
            LoadPosition();
        }
    }

    public void LoadPosition()
    {
        if (PlayerPrefs.HasKey(enemyID))
        {
            gameObject.SetActive(PlayerPrefs.GetInt(enemyID) == 1);
            float x = PlayerPrefs.GetFloat(enemyID + "_x", transform.position.x);
            float y = PlayerPrefs.GetFloat(enemyID + "_y", transform.position.y);
            transform.position = new Vector2(x, y);
        }
    }

    public void Died()
    {
        PlayerPrefs.SetInt(enemyID, 0); // Set to 0 when the enemy is dead
        PlayerPrefs.SetFloat(enemyID + "_x", transform.position.x);
        PlayerPrefs.SetFloat(enemyID + "_y", transform.position.y);
        gameObject.SetActive(false);
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat(enemyID + "_x", transform.position.x);
        PlayerPrefs.SetFloat(enemyID + "_y", transform.position.y);
        PlayerPrefs.SetInt(enemyID, gameObject.activeSelf ? 1 : 0); // Save the active state
    }

    public static void ResetAllEnemies()
    {
        newGame = true;
        EnemyManager.Instance?.ResetAllEnemies();
    }
}
