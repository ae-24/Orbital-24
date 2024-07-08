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
            // Check if CollectibleManager.Instance is not null before accessing it
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
    }
}
