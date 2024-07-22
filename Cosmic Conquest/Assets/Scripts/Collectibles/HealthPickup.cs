using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Collectible collectible = GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
            FindObjectOfType<GameSession>().AddHealth();
        }
    }
}
