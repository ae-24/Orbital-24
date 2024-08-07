using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure the GameObject persists across scenes
            // Ensure the music plays at the start
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate MusicManager GameObjects
        }
    }
    public void PlayMusic()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void StopMusic()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
