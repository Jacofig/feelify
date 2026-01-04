using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    public enum AudioType { Music, SFX }

    [Header("Slider Settings")]
    public AudioType audioType;            // wybór: Music lub SFX
    public Slider slider;                   // przypisz slider
    public TextMeshProUGUI percentageText; // przypisz tekst procentowy

    void Start()
    {
        // ustaw pocz¹tkow¹ wartoœæ i listener
        if (audioType == AudioType.Music)
        {
            slider.value = AudioManager.instance.GetMusicVolume();
            slider.onValueChanged.AddListener(AudioManager.instance.SetMusicVolume);
        }
        else // SFX
        {
            slider.value = AudioManager.instance.GetSFXVolume();
            slider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
        }

        // listener do aktualizacji tekstu
        slider.onValueChanged.AddListener(UpdateText);
        UpdateText(slider.value);
    }

    void UpdateText(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
