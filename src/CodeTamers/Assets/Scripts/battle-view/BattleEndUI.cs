using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleEndUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    public void SetResult(bool playerWon)
    {
        resultText.text = playerWon ? "VICTORY" : "DEFEAT";
    }

    public void OnRestartClicked()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitClicked()
    {
        SceneManager.LoadScene("OverworldScene");
    }
}
