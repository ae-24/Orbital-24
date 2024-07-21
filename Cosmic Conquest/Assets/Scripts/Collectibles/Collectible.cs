using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string collectibleID;

    void Start()
    {
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (CollectibleManager.Instance != null)
            {
                CollectibleManager.Instance.RegisterCollectible(collectibleID);
            }
            else
            {
                Debug.LogError("CollectibleManager instance is null. Ensure CollectibleManager is initialized before Collectible objects.");
            }
        }
    }

    public void Collect()
    {
        PlayerPrefs.SetInt(collectibleID, 1);
        gameObject.SetActive(false);
    }

    public static void ResetAllCollectibles()
    {
        CollectibleManager.Instance?.ResetAllCollectibles();
        Collectible[] allCollectibles = FindObjectsOfType<Collectible>();
        foreach (Collectible collectible in allCollectibles)
        {
            PlayerPrefs.SetInt(collectible.collectibleID, 0);
            collectible.gameObject.SetActive(true);
        }
        PlayerPrefs.Save();
    }
}
