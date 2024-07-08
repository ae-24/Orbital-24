using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //[SerializeField] AudioClip coinPickupSFX;
    //[SerializeField] int pointsForCoinPickup = 100;
    //AudioSource coinSound;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            Collectible collectible = GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
            //coinSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            wasCollected = true;
            FindObjectOfType<GameSession>().AddHealth();
            //coinSound.PlayOneShot(coinPickupSFX, 1f);
            gameObject.SetActive(false);
        }
    }
}
