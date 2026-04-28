using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForgeSceneLoader : MonoBehaviour
{
    public static ForgeSceneLoader Instance;

    [SerializeField] private string forgeScene = "ForgeScene";
    private Scene previousScene;

    private Dictionary<GameObject, bool> disabledObjects =
        new Dictionary<GameObject, bool>();

    private bool loaded = false;
    public Transform exitPoint; // ustaw w Inspectorze

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

    public void EnterForge(bool tutorial)
    {
        Time.timeScale = 1f;
        if (loaded)
            return;

        TutorialManager.ForgeTutorialActive = tutorial;

        previousScene = SceneManager.GetActiveScene();

        disabledObjects.Clear();

        foreach (var obj in previousScene.GetRootGameObjects())
        {
            // NIE wy³¹czaj obiektów oznaczonych jako Forge
            if (obj.CompareTag("Forge"))
                continue;

            
            if (obj.GetComponent<UnityEngine.EventSystems.EventSystem>() != null)
                continue;

            if (obj.GetComponent<PlayerInventory>() != null)
                continue;

            disabledObjects[obj] = obj.activeSelf;
            obj.SetActive(false);
        }



        SceneManager.LoadScene(forgeScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnLoaded;

        loaded = true;
    }

    // ================= LOADED =================

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == forgeScene)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnLoaded;

        }
    }

    // ================= EXIT =================

    public void ExitForge()
    {
        if (!loaded)
            return;

        SceneManager.UnloadSceneAsync(forgeScene);

        foreach (var kvp in disabledObjects)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        SceneManager.SetActiveScene(previousScene);

        // <<< TELEPORT >>>
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && exitPoint != null)
        {
            player.transform.position = exitPoint.position;
        }

        

        loaded = false;
    }
}