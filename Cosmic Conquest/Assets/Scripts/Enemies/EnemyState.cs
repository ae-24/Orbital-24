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
            if (PlayerPrefs.HasKey(enemyID))
            {
                gameObject.SetActive(PlayerPrefs.GetInt(enemyID) == 1);
            }
        }
    }

    public void Died()
    {
        PlayerPrefs.SetInt(enemyID, 1);
        gameObject.SetActive(false);
    }
    public static void ResetAllEnemies()
    {
        newGame = true;
        EnemyManager.Instance?.ResetAllEnemies();
    }
}
