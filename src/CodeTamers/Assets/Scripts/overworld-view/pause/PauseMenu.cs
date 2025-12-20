using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerSave player; 



    private bool isPaused = false;
    void Start()
    {
        // Upewniamy siê, ¿e gra nie jest zatrzymana
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        // Sprawdzenie czy gracz nacisn¹³ Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Wznów grê
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Zatrzymaj grê
        isPaused = true;
    }

    public void SaveGame()
    {
        // Tutaj dodaj logikê zapisu

        if (player != null)
        {
            player.SavePlayer(); // zapisujemy gracza
        }
        else
        {
            UnityEngine.Debug.LogWarning("Brak pod³¹czonego gracza do zapisu!");
        }

        


        UnityEngine.Debug.Log("Gra zapisana!");
        //PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        //PlayerPrefs.Save();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Wznów czas przed zmian¹ sceny
        SceneManager.LoadScene("MenuScene"); // Podmieñ "MainMenu" na nazwê swojej sceny
    }
}
