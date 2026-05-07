using UnityEngine;

public class DialogueAction_DestroySelf : MonoBehaviour, IDialogueAction
{
    public void Execute(System.Action onFinished)
    {
        SpawnedEnemy se = GetComponentInParent<SpawnedEnemy>();

        if (se != null)
        {
            if (se.spawner != null)
            {
                se.spawner.OnEnemyKilled();
            }

            Destroy(se.gameObject);
        }

        onFinished?.Invoke();
    }
}