using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected) {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z));
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }
}
