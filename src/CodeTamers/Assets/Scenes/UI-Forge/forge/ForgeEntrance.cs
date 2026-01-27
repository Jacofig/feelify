using UnityEngine;

public class ForgeEntrance : MonoBehaviour
{
    private bool isInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isInside)
            return;

        isInside = true;

        bool tutorialDone =
            QuestManager.Instance.IsQuestCompleted("ForgeTutorial");

        bool tutorial = !tutorialDone;

        ForgeSceneLoader.Instance.EnterForge(tutorial);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        isInside = false;
    }
}
