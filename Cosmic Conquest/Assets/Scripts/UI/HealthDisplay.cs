using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    int health;
    int maxHealth = 3; // Assuming max health is always 3

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    void Start()
    {
        StartCoroutine(UpdateHealth());
    }

    IEnumerator UpdateHealth()
    {
        while (true)
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (gameSession != null)
            {
                health = gameSession.playerLives;
                UpdateHearts();
            }
            else
            {
                Debug.LogWarning("GameSession not found!");
            }

            yield return new WaitForSeconds(0.5f); // Update health every 0.5 seconds
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
