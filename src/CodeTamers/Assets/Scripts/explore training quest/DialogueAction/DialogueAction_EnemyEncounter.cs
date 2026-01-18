using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueAction_EnemyEncounter : MonoBehaviour, IDialogueAction
{
    public PokemonData[] enemyTeam;

    private System.Action onFinishedCallback;
    private Scene previousScene;
    private Dictionary<GameObject, bool> originalActiveStates = new Dictionary<GameObject, bool>();

    public void Execute(System.Action onFinished)
    {
        onFinishedCallback = onFinished;

        BattleData.playerTeam = PlayerParty.Instance.party;
        BattleData.enemyTeam = BuildEnemyTeam();

        BattleData.onBattleFinished = OnBattleFinished;

        // Zapisujemy obecną scenę
        previousScene = SceneManager.GetActiveScene();

        // Zapisujemy stan wszystkich rootów i wyłączamy je
        originalActiveStates.Clear();
        foreach (var obj in previousScene.GetRootGameObjects())
        {
            originalActiveStates[obj] = obj.activeSelf;
            obj.SetActive(false);
        }

        BattlePause.SetMovementActive(false);

        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void OnBattleFinished(bool playerWon)
    {
        SceneManager.UnloadSceneAsync("BattleScene");

        // Przywracamy pierwotny stan rootów sceny gry
        foreach (var kvp in originalActiveStates)
        {
            if (kvp.Key != null) // w razie gdy obiekt został zniszczony
                kvp.Key.SetActive(kvp.Value);
        }

        BattlePause.SetMovementActive(true);

        onFinishedCallback?.Invoke();
    }

    private PokemonInstance[] BuildEnemyTeam()
    {
        PokemonInstance[] result = new PokemonInstance[enemyTeam.Length];

        for (int i = 0; i < enemyTeam.Length; i++)
        {
            if (enemyTeam[i] != null)
                result[i] = new PokemonInstance(enemyTeam[i]);
        }

        return result;
    }
}
