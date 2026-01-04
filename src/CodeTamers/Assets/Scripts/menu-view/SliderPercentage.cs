using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI percentageText;

    public enum AudioType { Music, SFX }
    public AudioType audioType;

    void Start()
    {
        float initialValue;
        switch (audioType)
        {
            case AudioType.Music:
                initialValue = PlayerPrefs.GetFloat("MusicVolume", AudioManager.instance.GetMusicVolume());
                slider.value = initialValue;
                slider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
                break;
            case AudioType.SFX:
                initialValue = PlayerPrefs.GetFloat("SFXVolume", AudioManager.instance.GetSFXVolume());
                slider.value = initialValue;
                slider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
                break;
        }

        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
        slider.onValueChanged.AddListener(SaveValue);
    }

    void UpdateText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        percentageText.text = percent + "%";
    }

    void SaveValue(float value)
    {
        switch (audioType)
        {
            case AudioType.Music:
                PlayerPrefs.SetFloat("MusicVolume", value);
                break;
            case AudioType.SFX:
                PlayerPrefs.SetFloat("SFXVolume", value);
                break;
        }
        PlayerPrefs.Save();
    }
}
