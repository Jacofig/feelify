using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirewallMiniGameSceneLoader : MonoBehaviour
{
    public static FirewallMiniGameSceneLoader Instance;

    [SerializeField] private string miniGameScene = "minigame1";

    private Scene previousScene;

    private Dictionary<GameObject, bool> disabledObjects =
        new Dictionary<GameObject, bool>();

    private bool loaded = false;

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

    public void EnterMiniGame()
    {
        if (loaded)
            return;

        previousScene = SceneManager.GetActiveScene();

        disabledObjects.Clear();

        foreach (var obj in previousScene.GetRootGameObjects())
        {
            // NIE wy³¹czaj EventSystem (UI input)
            if (obj.GetComponent<UnityEngine.EventSystems.EventSystem>() != null)
                continue;

            // NIE wy³¹czaj inventory (KLUCZOWE)
            if (obj.GetComponent<PlayerInventory>() != null)
                continue;

            disabledObjects[obj] = obj.activeSelf;
            obj.SetActive(false);
        }

        SceneManager.LoadScene(miniGameScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnLoaded;

        loaded = true;
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == miniGameScene)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnLoaded;
        }
    }

    // ================= EXIT =================

    public void ExitMiniGame()
    {
        if (!loaded)
            return;

        SceneManager.UnloadSceneAsync(miniGameScene);

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
    }
}