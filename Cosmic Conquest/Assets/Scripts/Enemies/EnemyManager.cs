using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private HashSet<string> enemyIDs = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy(string id)
    {
        if (!enemyIDs.Contains(id))
        {
            enemyIDs.Add(id);
        }
    }

    public void ResetAllEnemies()
    {
        foreach (var id in enemyIDs)
        {
            PlayerPrefs.DeleteKey(id);
            PlayerPrefs.DeleteKey(id + "_x");
            PlayerPrefs.DeleteKey(id + "_y");
        }
        PlayerPrefs.Save();
    }

    public void SaveAllEnemyPositions()
    {
        foreach (string id in enemyIDs)
        {
            GameObject enemy = GameObject.Find(id);
            if (enemy != null)
            {
                EnemyState enemyState = enemy.GetComponent<EnemyState>();
                if (enemyState != null)
                {
                    enemyState.SavePosition();
                }
            }
        }
        PlayerPrefs.Save();
    }

    public void LoadAllEnemyPositions()
    {
        foreach (string id in enemyIDs)
        {
            GameObject enemy = GameObject.Find(id);
            if (enemy != null)
            {
                EnemyState enemyState = enemy.GetComponent<EnemyState>();
                if (enemyState != null)
                {
                    enemyState.LoadPosition();
                }
            }
        }
    }
}
