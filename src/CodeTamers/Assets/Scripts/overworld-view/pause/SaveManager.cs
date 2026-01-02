using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadAll();
    }

    public void SaveAll()
    {
        SaveableObject[] objects = FindObjectsOfType<SaveableObject>();

        foreach (SaveableObject obj in objects)
        {
            obj.Save();
        }

        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        UnityEngine.Debug.Log("Zapisano wszystkie obiekty");
    }

    public void LoadAll()
    {
        SaveableObject[] objects = FindObjectsOfType<SaveableObject>();

        foreach (SaveableObject obj in objects)
        {
            obj.Load();
        }

        UnityEngine.Debug.Log("Wczytano wszystkie obiekty");
    }
}
