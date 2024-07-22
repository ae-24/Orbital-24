using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string collectibleID;
    private static bool newGame = false;

    void Start()
    {
        if (newGame) 
        {
            PlayerPrefs.DeleteKey(collectibleID);
            gameObject.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.HasKey(collectibleID))
            {
                gameObject.SetActive(PlayerPrefs.GetInt(collectibleID) == 0);
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
        newGame = true;
        CollectibleManager.Instance?.ResetAllCollectibles();
    }
}
