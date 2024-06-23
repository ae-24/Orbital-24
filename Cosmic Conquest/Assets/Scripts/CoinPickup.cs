using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(coinPickupSFX, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z));
            Destroy(gameObject);
        }    
    }
}
