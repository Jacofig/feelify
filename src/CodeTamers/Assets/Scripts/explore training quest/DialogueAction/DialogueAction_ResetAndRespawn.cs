using UnityEngine;
using System.Collections;

public class DialogueAction_ResetAndRespawn : MonoBehaviour, IDialogueAction
{
    public DialogueTrigger targetTrigger;
    public float respawnTime = 20f;

    public bool resetStage = true;

    public void Execute(System.Action onFinished)
    {
        StartCoroutine(Run(onFinished));
    }

    IEnumerator Run(System.Action onFinished)
    {
        // 🔥 reset dialogu
        if (targetTrigger != null && resetStage)
        {
            targetTrigger.currentStage = 0;

            foreach (var stage in targetTrigger.stages)
            {
                stage.hasRun = false;
            }
        }

        // ⏳ czekanie
        yield return new WaitForSeconds(respawnTime);

        // 🔥 przywrócenie obiektu dialogu
        gameObject.SetActive(true);

        onFinished?.Invoke();
    }
}