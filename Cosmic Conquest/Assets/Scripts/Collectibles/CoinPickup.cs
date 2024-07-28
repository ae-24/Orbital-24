using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    AudioSource coinSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Collectible collectible = GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
            else
            {
                Debug.LogWarning("Collectible component not found on coin pickup.");
            }

            coinSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            if (coinSound != null)
            {
                FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
                coinSound.PlayOneShot(coinPickupSFX, 1f);
            }
            else
            {
                Debug.LogWarning("AudioSource for coin pickup sound not found.");
            }
        }
    }
}
