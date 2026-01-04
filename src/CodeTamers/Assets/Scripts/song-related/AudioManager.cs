using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;  // dla muzyki
    [SerializeField] private AudioSource sfxSource;    // dla SFX

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

    // tylko muzyka, SFX bêdzie mieæ w³asny g³oœnoœæ jeœli potrzebujesz
    public void SetMusicVolume(float value)
    {
        musicSource.volume = Mathf.Clamp01(value);
    }

    public float GetMusicVolume()
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

    // ================= SFX =================

    // odtwarza jeden dŸwiêk efektu
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // jeœli chcesz, mo¿esz te¿ mieæ oddzielny suwak dla SFX
    public void SetSFXVolume(float value)
    {
        sfxSource.volume = Mathf.Clamp01(value);
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }
}
