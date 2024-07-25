using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private static CollectibleManager instance;
    public static CollectibleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CollectibleManager>();
            }
            return instance;
        }
    }

    private List<string> collectibleIDs = new List<string>();

    public void RegisterCollectible(string collectibleID)
    {
        if (!collectibleIDs.Contains(collectibleID))
        {
            collectibleIDs.Add(collectibleID);
        }
    }

    public void ResetAllCollectibles()
    {
        foreach (var collectibleID in collectibleIDs)
        {
            PlayerPrefs.DeleteKey(collectibleID);
        }
    }
}
