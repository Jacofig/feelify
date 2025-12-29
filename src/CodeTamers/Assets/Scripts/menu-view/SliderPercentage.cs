using UnityEngine;
using UnityEngine.UI;
using TMPro; // jeœli u¿ywasz TextMeshPro

public class SliderPercentage : MonoBehaviour
{
    public Slider slider;               // Twój slider
    public TextMeshProUGUI percentageText; // Tekst pokazuj¹cy procent (mo¿e byæ te¿ Text zamiast TMP)

    void Start()
    {
        // ustaw pocz¹tkowy tekst
        UpdateText(slider.value);

        // dodaj listener do slidera
        slider.onValueChanged.AddListener(UpdateText);
    }

    // funkcja aktualizuj¹ca tekst
    void UpdateText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100); // przelicz 0-1 na 0-100%
        percentageText.text = percent + "%";
    }
}
