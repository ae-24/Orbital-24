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
            // Check if CollectibleManager.Instance is not null before accessing it
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
    }

}
