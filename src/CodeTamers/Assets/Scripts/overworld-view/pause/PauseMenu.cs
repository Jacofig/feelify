using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsPanel;
    public GameObject audioPanel;
    public GameObject controlsPanel;



    private bool isPaused = false;
    void Start()
    {
        // Upewniamy siê, ¿e gra nie jest zatrzymana
        pauseMenuUI.SetActive(false);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume(); 
            }
        }
    }

    
    public void Resume()
    {
        CloseAllPanels();
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        CloseAllPanels();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void OpenOptionsPanel()
    {
        pauseMenuUI.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void OpenAudioPanel()
    {
        pauseMenuUI.SetActive(false);
        audioPanel.SetActive(true);
    }

    public void CloseAudioPanel()
    {
        audioPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void OpenControlsPanel()
    {
        pauseMenuUI.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControlsPanel()
    {
        controlsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    void CloseAllPanels()
    {
        pauseMenuUI.SetActive(false);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
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
