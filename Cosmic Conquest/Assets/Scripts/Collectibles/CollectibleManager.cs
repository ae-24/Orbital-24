using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    private HashSet<string> collectibleIDs = new HashSet<string>();

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

    public void RegisterCollectible(string id)
    {
        if (!collectibleIDs.Contains(id))
        {
            collectibleIDs.Add(id);
        }
    }

    public void ResetAllCollectibles()
    {
        foreach (var id in collectibleIDs)
        {
            collectibleIDs.Remove(id);
        }
        PlayerPrefs.Save();
    }
}
