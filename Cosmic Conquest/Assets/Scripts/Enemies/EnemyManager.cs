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
            enemyIDs.Remove(id);
        }
        PlayerPrefs.Save();
    }
}
