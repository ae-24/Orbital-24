using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private HashSet<string> enemyIDs = new HashSet<string>();
    private List<GameObject> enemies = new List<GameObject>();

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

    public void RegisterEnemy(GameObject enemy, string id)
    {
        if (enemy == null)
        {
            Debug.LogError("Attempted to register a null enemy.");
            return;
        }

        var enemyState = enemy.GetComponent<EnemyState>();
        if (enemyState == null)
        {
            Debug.LogError($"Enemy {enemy.name} does not have an EnemyState component.");
            return;
        }

        if (!enemyIDs.Contains(id))
        {
            Debug.Log($"Registering enemy with ID: {id}");
            enemyIDs.Add(id);
            enemies.Add(enemy);
        }
        else
        {
            Debug.LogWarning($"Enemy with ID {id} is already registered.");
        }
    }


    public void ResetAllEnemies()
    {
        Debug.Log("Resetting all enemies");
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
        Debug.Log("Saving all enemy positions");
        foreach (GameObject enemy in enemies)
        {
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
        Debug.Log("Loading all enemy positions");
        foreach (GameObject enemy in enemies)
        {
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

    public static void ResetInstance()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance.gameObject);
            Instance = null;
        }
    }
}