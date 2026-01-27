using UnityEngine;

public class ForgeDialogueSwitcher : MonoBehaviour
{
    private DialogueTrigger trigger;

    void OnEnable()
    {
        trigger = GetComponent<DialogueTrigger>();

        if (QuestManager.Instance != null &&
            QuestManager.Instance.IsQuestCompleted("ForgeTutorial"))
        {
            trigger.currentStage = 1;
        }
        else
        {
            trigger.currentStage = 0;
        }
    }

}
