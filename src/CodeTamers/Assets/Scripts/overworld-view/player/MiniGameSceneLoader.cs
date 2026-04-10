using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneLoader : MonoBehaviour
{
    public static MiniGameSceneLoader Instance;

    [SerializeField] private string minigameScene = "minigame1";
    private string previousSceneName = "OverWorldScene";

    private Dictionary<GameObject, bool> disabledObjects =
        new Dictionary<GameObject, bool>();




    private bool loaded = false;
    private bool canEnter = true;

    public Transform exitPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ================= ENTER =================

    public void Enterminigame()
    {
        if (loaded || !canEnter)
            return;

        previousSceneName = SceneManager.GetActiveScene().name;

        disabledObjects.Clear();

        Scene previousScene = SceneManager.GetSceneByName(previousSceneName);

        foreach (var obj in previousScene.GetRootGameObjects())
        {
            disabledObjects[obj] = obj.activeSelf;
            obj.SetActive(false);
        }

        SceneManager.LoadScene(minigameScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnLoaded;

        loaded = true;
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == minigameScene)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnLoaded;
        }
    }

    // ================= EXIT =================

    public void Exitminigame()
    {
        if (!loaded)
            return;

        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        UnityEngine.Debug.Log("Exit minigame start");

        SceneManager.UnloadSceneAsync(minigameScene);

        // czekaj a¿ scena minigry siê usunie
        Scene scene = SceneManager.GetSceneByName(minigameScene);
        while (scene.isLoaded)
            yield return null;

        // przywróæ aktywnoœæ obiektów OverWorld
        foreach (var kvp in disabledObjects)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        // wróæ do sceny OverWorldScene
        SceneManager.SetActiveScene(
            SceneManager.GetSceneByName(previousSceneName)
        );

        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && exitPoint != null)
        {
            player.transform.position = exitPoint.position;
        }

        UnityEngine.Debug.Log("Exit minigame done");

        loaded = false;
        StartCoroutine(CooldownRoutine(5f));
    }

    private IEnumerator CooldownRoutine(float seconds)
    {
        canEnter = false;
        yield return new WaitForSeconds(seconds);
        canEnter = true;
    }
}