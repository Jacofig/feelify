using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime = 20f;
    float timer;

    public int hp = 5;

    public bool gameEnded = false;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI statusText;
    public RewardGiver rewardGiver;
    void Awake()
    {
        instance = this;
        statusText.text = "PROTECT THE NETWORK";
    }

    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= gameTime)
        {
            WinGame();
        }

        timerText.text = "TIME: " + Mathf.Ceil(gameTime - timer);
        hpText.text = "INTEGRITY: " + hp;
    }

    public void VirusPassed()
    {
        hp--;

        if (hp <= 0)
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        gameEnded = true;
        statusText.text = "SUCCESS";
        rewardGiver?.GiveRewards();
        Time.timeScale = 0f;
        StartCoroutine(ExitAfterDelay());
    }

    public void LoseGame()
    {
        gameEnded = true;
        statusText.text = "FAIL";
        Time.timeScale = 0f;

        StartCoroutine(ExitAfterDelay());
    }

    private void Cleanup()
    {
        Time.timeScale = 1f;

        var spawners = Object.FindObjectsByType<AttackSpawner>(FindObjectsSortMode.None);
        foreach (var s in spawners)
        {
            s.StopAllCoroutines();
        }
    }

    private IEnumerator ExitAfterDelay()
    {

        yield return new WaitForSecondsRealtime(3f);

        Cleanup();

        if (FirewallMiniGameSceneLoader.Instance != null)
        {
            FirewallMiniGameSceneLoader.Instance.ExitMiniGame();
        }
    }
}