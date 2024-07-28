using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    private HashSet<string> registeredCollectibles = new HashSet<string>();

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

    public void RegisterCollectible(string collectibleID)
    {
        if (!registeredCollectibles.Contains(collectibleID))
        {
            registeredCollectibles.Add(collectibleID);
            Debug.Log($"Registered collectible: {collectibleID}");
        }
    }

    public void ResetAllCollectibles()
    {
        foreach (var collectibleID in registeredCollectibles)
        {
            PlayerPrefs.DeleteKey(collectibleID);
            Debug.Log($"Reset collectible: {collectibleID}");
        }
        PlayerPrefs.Save();
    }
}
