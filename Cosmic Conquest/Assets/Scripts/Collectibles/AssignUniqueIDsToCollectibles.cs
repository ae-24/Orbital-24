using UnityEditor;
using UnityEngine;

public class AssignUniqueIDsToCollectibles : MonoBehaviour
{
    [MenuItem("Tools/Assign Unique IDs to All Collectibles")]
    public static void AssignUniqueIDs()
    {
        Collectible[] collectibles = FindObjectsOfType<Collectible>();
        foreach (Collectible collectible in collectibles)
        {
            collectible.collectibleID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(collectible);
        }

        Debug.Log("Assigned unique IDs to all collectibles.");
    }
}
