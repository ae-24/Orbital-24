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
            else
            {
                Debug.Log($"{collectibleID} does not exist in PlayerPrefs. Setting active.");
                gameObject.SetActive(true);
            }
        }

        CollectibleManager.Instance.RegisterCollectible(collectibleID);
    }

    public void Collect()
    {
        PlayerPrefs.SetInt(collectibleID, 1);
        gameObject.SetActive(false);
        Debug.Log($"{collectibleID} collected and set inactive.");
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
        Debug.Log($"{collectibleID} removed from PlayerPrefs on destroy.");
    }
}
