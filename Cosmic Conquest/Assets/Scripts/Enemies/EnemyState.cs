using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public string enemyID;

    void Start()
    {
        if (PlayerPrefs.GetInt(enemyID, 0) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.RegisterEnemy(enemyID);
            }
            else
            {
                Debug.LogError("EnemyManager instance is null.");
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
        EnemyManager.Instance?.ResetAllEnemies();
        EnemyState[] allEnemies = FindObjectsOfType<EnemyState>();
        foreach (EnemyState enemy in allEnemies)
        {
            PlayerPrefs.SetInt(enemy.enemyID, 0);
            enemy.gameObject.SetActive(true);
        }
        PlayerPrefs.Save();
    }
}
