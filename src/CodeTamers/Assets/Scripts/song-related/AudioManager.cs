using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource; // AudioSource do muzyki

    void Awake()
    {
        // Singleton – tylko jeden AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.Play();
    }

    // Zmiana muzyki
    public void ChangeMusic(AudioClip newClip)
    {
        musicSource.clip = newClip;
        musicSource.Play();
    }
}
