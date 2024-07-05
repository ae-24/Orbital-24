using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public Transform player;
    Vector2 playerPosition;

    public void LoadPos()
    {
        float posX = PlayerPrefs.GetFloat("playerPositionX", player.position.x);
        float posY = PlayerPrefs.GetFloat("playerPositionY", player.position.y);

        Debug.Log("Loading Player Position: X: " + posX + " Y: " + posY);

        if (player != null)
        {
            player.position = new Vector2(posX, posY);
            Debug.Log("Player position set to: " + player.position);
        }
        else
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    public void SavePos()
    {
        playerPosition = player.position;
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        Debug.Log("Saved Player Position: X: " + playerPosition.x + " Y: " + playerPosition.y);
    }
}
