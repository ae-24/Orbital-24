using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3; // Number of player lives

    void Awake()
    {
        //Ensures that there is at most one game session running to keep track of player data
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    //Resets game session when player runs out of lives
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1) {
            TakeLife();
        } else {
            ResetGameSession();
        }
    }

    //Reduces player lives count when player dies, and resets them to the start of their current level
    void TakeLife()
    {
        playerLives -= 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //Destroys current game session and reinitializes to the first level
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
