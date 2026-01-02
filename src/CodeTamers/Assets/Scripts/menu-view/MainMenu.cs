using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject audioPanel;
    public GameObject controlsPanel;

    void Start()
    {
        // Na starcie w³¹cz tylko g³ówne menu
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("IntroScene"); // NAZWA SCENY
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
        }
        else
        {
            SceneManager.LoadScene("OverworldScene");
        }
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void OpenAudio()
    {
        optionsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }
    public void CloseAudio()
    {
        audioPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void OpenControls()
    {
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Game Quit"); // dzia³a tylko w buildzie
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // zatrzymanie gry w edytorze
#endif
    }
}
