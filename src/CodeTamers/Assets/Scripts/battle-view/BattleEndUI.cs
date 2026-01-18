using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleEndUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    private bool? playerVictory = null;

    public void SetResult(bool playerWon)
    {
        playerVictory = playerWon;
        resultText.text = playerWon ? "VICTORY" : "DEFEAT";
    }

    public void OnRestartClicked()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitClicked()
    {
        //SceneManager.LoadScene("OverworldScene");

        if (playerVictory.HasValue)
        {
            // Wywo³anie callbacka bitwy przy wychodzeniu
            BattleData.onBattleFinished?.Invoke(playerVictory.Value);
            BattleData.onBattleFinished = null;
        }
    }
}
