using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public Transform player;
    Vector2 playerPosition;

    void Start()
    {
        Debug.Log("PlayerPos script initialized.");
    }

    public void LoadPos()
    {
        Debug.Log("X:" + PlayerPrefs.GetFloat("playerPositionX") + " Y: " + PlayerPrefs.GetFloat("playerPositionY"));
        player.position = new Vector2(PlayerPrefs.GetFloat("playerPositionX"), PlayerPrefs.GetFloat("playerPositionY"));
    }

    public void SavePos()
    {
        playerPosition = player.position;
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        Debug.Log("X:" + PlayerPrefs.GetFloat("playerPositionX") + " Y: " + PlayerPrefs.GetFloat("playerPositionY"));
    }
}
