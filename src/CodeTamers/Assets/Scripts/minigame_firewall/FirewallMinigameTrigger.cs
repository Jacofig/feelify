using UnityEngine;

public class FirewallMinigameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        FirewallMiniGameSceneLoader.Instance.EnterMiniGame();
    }
}