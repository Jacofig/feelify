using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private float masterVolume = 0.5f;
    private float baseMusicVolume = 0.2f;
    private float baseSFXVolume = 0.2f;

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

    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        UpdateVolumes();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public void SetMusicVolume(float value)
    {
        baseMusicVolume = Mathf.Clamp01(value);
        UpdateVolumes();
    }

    public float GetMusicVolume()
    {
        return baseMusicVolume;
    }

    public void SetSFXVolume(float value)
    {
        baseSFXVolume = Mathf.Clamp01(value);
        UpdateVolumes();
    }

    public float GetSFXVolume()
    {
        return baseSFXVolume;
    }

    private void UpdateVolumes()
    {
        musicSource.volume = baseMusicVolume * masterVolume;
        sfxSource.volume = baseSFXVolume * masterVolume;
    }





    // ================= MUSIC =================

    public void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip)
            return;

        musicSource.clip = newClip;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}
