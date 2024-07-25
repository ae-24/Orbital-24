using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string collectibleID;
    private static bool newGame = false;

    public void Start()
    {
        if (newGame)
        {
            PlayerPrefs.DeleteKey(collectibleID);
            gameObject.SetActive(true);
            Debug.Log($"New game, resetting {collectibleID}");
        }
        else
        {
            if (PlayerPrefs.HasKey(collectibleID))
            {
                bool isActive = PlayerPrefs.GetInt(collectibleID) == 0;
                gameObject.SetActive(isActive);
                Debug.Log($"{collectibleID} set active: {isActive}");
            }
        }

        CollectibleManager.Instance.RegisterCollectible(collectibleID);
    }
    
    public void Collect()
    {
        PlayerPrefs.SetInt(collectibleID, 1);
        Debug.Log($"Collected {collectibleID}, setting active to false.");
        gameObject.SetActive(false);
    }

    public static void ResetAllCollectibles()
    {
        newGame = true;
        CollectibleManager.Instance?.ResetAllCollectibles();
    }

    public static void ResetNewGameFlag()
    {
        newGame = false;
    }

    private void OnDestroy()
    {
        PlayerPrefs.DeleteKey(collectibleID);
    }
}
