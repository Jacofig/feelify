using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueStage
{
    public Dialogue dialogue;           // mo¿e byæ null
    public MonoBehaviour[] actions;     // komponenty implementuj¹ce IDialogueAction
    [HideInInspector] public bool hasRun;

   
    public UnityEvent onStageFinished;
}
