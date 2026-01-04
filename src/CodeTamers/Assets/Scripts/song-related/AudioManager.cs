using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioMixer audioMixer;

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

    // ================= MUSIC =================

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp01(value)) * 20f);
    }

    public float GetMusicVolume()
    {
        float vol;
        audioMixer.GetFloat("MusicVolume", out vol);
        return Mathf.Pow(10, vol / 20f);
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return;
        musicSource.clip = newClip;
        musicSource.Play();
    }

    // ================= SFX =================

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp01(value)) * 20f);
    }

    public float GetSFXVolume()
    {
        float vol;
        audioMixer.GetFloat("SFXVolume", out vol);
        return Mathf.Pow(10, vol / 20f);
    }
}
