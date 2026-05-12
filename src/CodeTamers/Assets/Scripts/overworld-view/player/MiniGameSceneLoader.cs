using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneLoader : MonoBehaviour
{
    public static MiniGameSceneLoader Instance;

    [SerializeField] private string minigameScene = "minigame1";

    private Scene previousScene;

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

        previousScene = SceneManager.GetActiveScene();

        disabledObjects.Clear();

        foreach (var obj in previousScene.GetRootGameObjects())
        {
            // NIE wy³¹czaj EventSystem
            if (obj.GetComponent<UnityEngine.EventSystems.EventSystem>() != null)
                continue;

            // NIE wy³¹czaj inventory
            if (obj.GetComponent<PlayerInventory>() != null)
                continue;

            disabledObjects[obj] = obj.activeSelf;
            obj.SetActive(false);
        }

        SceneManager.LoadScene(minigameScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnLoaded;

        loaded = true;
    }

    // ================= LOADED =================

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
        AsyncOperation unload =
            SceneManager.UnloadSceneAsync(minigameScene);

        while (!unload.isDone)
            yield return null;

        foreach (var kvp in disabledObjects)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        SceneManager.SetActiveScene(previousScene);

        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && exitPoint != null)
        {
            player.transform.position = exitPoint.position;
        }

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