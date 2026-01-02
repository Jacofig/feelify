using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource musicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    // ================= VOLUME =================

    public void SetVolume(float value)
    {
        musicSource.volume = Mathf.Clamp01(value);
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }

    // ================= MUSIC =================

    public void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip)
            return;

        musicSource.clip = newClip;
        musicSource.Play();
    }
}
