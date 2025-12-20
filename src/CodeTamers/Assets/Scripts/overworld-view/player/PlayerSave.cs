using System.Diagnostics;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public float health = 100f; // przyk³adowa zmienna stanu gracza

    void Start()
    {
        LoadPlayer();
    }

    // Zapisujemy pozycjê i stan gracza
    public void SavePlayer()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

        PlayerPrefs.SetFloat("PlayerHealth", health);

        // Zapis sceny
        PlayerPrefs.SetString("LastScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();
        UnityEngine.Debug.Log("Gracz zapisany!");
    }

    // Wczytanie pozycji i stanu
    public void LoadPlayer()
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);

            health = PlayerPrefs.GetFloat("PlayerHealth");

            UnityEngine.Debug.Log("Gracz wczytany!");
        }
        else
        {
            UnityEngine.Debug.Log("Brak zapisanych danych gracza.");
        }
    }
}
