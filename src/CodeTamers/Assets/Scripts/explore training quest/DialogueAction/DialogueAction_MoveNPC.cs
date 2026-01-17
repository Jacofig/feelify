using UnityEngine;

public class DialogueAction_MoveNPC : MonoBehaviour, IDialogueAction
{
    public NPCMovement npc;

    public void Execute()
    {
        npc.MoveToTarget();
    }
}
