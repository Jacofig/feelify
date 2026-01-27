using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueAction_LoadForgeScene : MonoBehaviour, IDialogueAction
{
    [SerializeField]
    private string forgeSceneName = "ForgeScene";

    private System.Action onFinishedCallback;

    private Scene previousScene;

    private Dictionary<GameObject, bool> originalActiveStates = new();
    private static DialogueAction_LoadForgeScene activeLoader;

    public void Execute(System.Action callback)
    {
        activeLoader = this;

        onFinishedCallback = callback;

        // tutorial / normal
        bool tutorialDone =
            QuestManager.Instance.IsQuestCompleted("ForgeTutorial");

        TutorialManager.ForgeTutorialActive = !tutorialDone;

        Debug.Log("Forge tutorial active = " + TutorialManager.ForgeTutorialActive);

        // zapisz scene
        previousScene = SceneManager.GetActiveScene();

        // wy³¹cz rooty
        originalActiveStates.Clear();

        foreach (var obj in previousScene.GetRootGameObjects())
        {
            // NIE wy³¹czamy forga
            if (obj.CompareTag("Forge"))
                continue;

            originalActiveStates[obj] = obj.activeSelf;
            obj.SetActive(false);
        }


        // load additive
        SceneManager.LoadScene(forgeSceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == forgeSceneName)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnSceneLoaded;

            onFinishedCallback?.Invoke();
        }
    }

    public void ExitForge()
    {
        SceneManager.UnloadSceneAsync(forgeSceneName);

        foreach (var kvp in originalActiveStates)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        var trigger = FindObjectOfType<DialogueTrigger>();

        if (trigger != null)
        {
            if (trigger.currentStage < trigger.stages.Length)
            {
                trigger.stages[trigger.currentStage].hasRun = false;
            }
        }
        else
        {
            Debug.LogError("No DialogueTrigger found when exiting forge!");
        }

        onFinishedCallback?.Invoke();

        activeLoader = null;
    }



    public static DialogueAction_LoadForgeScene GetActive()
    {
        return activeLoader;
    }

}
