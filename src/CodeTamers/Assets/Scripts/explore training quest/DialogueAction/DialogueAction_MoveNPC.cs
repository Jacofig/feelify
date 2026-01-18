using UnityEngine;

public class DialogueAction_MoveNPC : MonoBehaviour, IDialogueAction
{
    public NPCMovement npc;

    public void Execute(System.Action onFinished)
    {
        npc.MoveToTarget();

        // jeœli ruch jest natychmiastowy LUB
        // MoveToTarget samo odpala coroutine/Update
        onFinished?.Invoke();
    }
}
