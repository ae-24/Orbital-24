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
            coinSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            coinSound.PlayOneShot(coinPickupSFX, 1f);
        }
    }
}
