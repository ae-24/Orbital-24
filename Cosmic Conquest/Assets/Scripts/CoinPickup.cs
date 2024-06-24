using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
<<<<<<< Updated upstream
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(coinPickupSFX, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z));
=======
    [SerializeField] int audioVolume = 50;
    [SerializeField] int pointsForCoinPickup = 100;
    AudioSource coinSound;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected) {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            coinSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            coinSound.PlayOneShot(coinPickupSFX, audioVolume);
            gameObject.SetActive(false);
>>>>>>> Stashed changes
            Destroy(gameObject);
        }    
    }
}
