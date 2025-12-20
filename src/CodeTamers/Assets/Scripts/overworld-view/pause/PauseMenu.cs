using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
   



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
       

        SaveManager.Instance.SaveAll();


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
