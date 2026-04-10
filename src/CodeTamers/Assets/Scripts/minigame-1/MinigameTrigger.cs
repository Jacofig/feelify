using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MiniGameSceneLoader.Instance.Enterminigame();
        }
    }
}