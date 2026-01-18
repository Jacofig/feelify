using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueAction_EnemyEncounter : MonoBehaviour, IDialogueAction
{
    public PokemonData[] enemyTeam;

    private System.Action onFinishedCallback;

    public void Execute(System.Action onFinished)
    {
        UnityEngine.Debug.Log(" EnemyEncounter Execute");

        onFinishedCallback = onFinished;

        BattleData.playerTeam = PlayerParty.Instance.party;
        BattleData.enemyTeam = BuildEnemyTeam();

        BattleData.onBattleFinished = OnBattleFinished;


        foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            obj.SetActive(false);
        }
        BattlePause.SetMovementActive(false);


        //SceneManager.LoadScene("BattleScene");
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Additive);
       SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            SceneManager.SetActiveScene(scene); // aktywna scena = BattleScene
            SceneManager.sceneLoaded -= OnSceneLoaded; // odsubskrybuj, żeby nie wywoływać ponownie
        }
    }

    public void OnBattleFinished(bool playerWon)
    {
        
       
        
        SceneManager.UnloadSceneAsync("BattleScene");

        // włącz z powrotem obiekty starej sceny
        foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            obj.SetActive(true);
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
