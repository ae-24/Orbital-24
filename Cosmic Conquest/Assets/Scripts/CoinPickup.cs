using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    AudioSource coinSound;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected) {
            coinSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            coinSound.PlayOneShot(coinPickupSFX, 1f);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }
}
