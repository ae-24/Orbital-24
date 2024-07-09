using UnityEditor;
using UnityEngine;

public class AssignUniqueIDsToEnemies : MonoBehaviour
{
    [MenuItem("Tools/Assign Unique IDs to All Enemies")]
    public static void AssignUniqueIDs()
    {
        EnemyMovement[] enemyMovements = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemyMovements)
        {
            enemy.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemy);
        }

        Debug.Log("Assigned unique IDs to all enemies.");
    }
}
