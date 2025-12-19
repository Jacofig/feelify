using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    public void NewGame()
    {
        SceneManager.LoadScene("OverworldScene"); // NAZWA SCENY
    }

    public void LoadGame()
    {
        // na razie nic
        UnityEngine.Debug.Log("Load Game - not implemented");
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

    public void ExitGame()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Game Quit"); // dzia³a tylko w buildzie
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // zatrzymanie gry w edytorze
#endif
    }
}
