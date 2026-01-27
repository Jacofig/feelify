using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    public enum AudioType { Master, Music, SFX }

    [Header("Slider Settings")]
    public AudioType audioType;            // wybór: Music lub SFX
    public Slider slider;                   // przypisz slider
    public TextMeshProUGUI percentageText; // przypisz tekst procentowy

    void Start()
    {
        if (slider == null)
        {
            Debug.LogWarning("AudioSlider: Slider not assigned!", this);
            return;
        }

        if (percentageText == null)
        {
            Debug.LogWarning("AudioSlider: PercentageText not assigned!", this);
            return;
        }

        if (AudioManager.instance == null)
        {
            Debug.LogWarning("AudioSlider: AudioManager instance not found!", this);
            return;
        }

        switch (audioType)
        {
            case AudioType.Master:
                slider.value = AudioManager.instance.GetMasterVolume();
                slider.onValueChanged.AddListener(AudioManager.instance.SetMasterVolume);
                break;

            case AudioType.Music:
                slider.value = AudioManager.instance.GetMusicVolume();
                slider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
                break;

            case AudioType.SFX:
                slider.value = AudioManager.instance.GetSFXVolume();
                slider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
                break;
        }

        slider.onValueChanged.AddListener(UpdateText);
        UpdateText(slider.value);
    }


    void UpdateText(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
