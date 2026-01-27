using UnityEngine;

public class ForgeTutorialController : MonoBehaviour
{
    [SerializeField] private ForgeManager forgeManager;

    [Header("Tutorial Quest IDs")]
    public string[] tutorialSteps;

    [SerializeField] private Dialogue startDialogue;

    [SerializeField] private Dialogue[] instructionDialogues;
    [SerializeField] private Dialogue[] afterCraftDialogues;

    [SerializeField] private Dialogue finishDialogue;

    [SerializeField] private DialogueManager dialogueManager;


    private int currentStep = 0;


    private void ShowInstruction()
    {
        if (currentStep < instructionDialogues.Length &&
            instructionDialogues[currentStep] != null)
        {
            dialogueManager.StartDialogue(instructionDialogues[currentStep]);
        }
    }

    void Start()
    {
        if (!TutorialManager.ForgeTutorialActive)
        {
            gameObject.SetActive(false);
            return;
        }

        forgeManager.OnForgeResult += OnForgeResult;

        Debug.Log("Forge Tutorial Active");

        if (startDialogue != null)
        {
            dialogueManager.StartDialogue(startDialogue);
        }
        ShowInstruction();
    }

    void OnDestroy()
    {
        if (forgeManager != null)
            forgeManager.OnForgeResult -= OnForgeResult;
    }



    private void FinishTutorial()
    {
        if (finishDialogue != null)
        {
            dialogueManager.StartDialogue(finishDialogue);
        }

        Debug.Log("Forge tutorial finished");

        TutorialManager.ForgeTutorialActive = false;

        forgeManager.OnForgeResult -= OnForgeResult;
    }



    public string GetCurrentStepId()
    {
        if (currentStep >= tutorialSteps.Length)
            return null;

        return tutorialSteps[currentStep];
    }

    private void OnForgeResult(ForgeResultType result)
    {
        if (result != ForgeResultType.Success)
            return;

        if (currentStep >= tutorialSteps.Length)
            return;

        string target = tutorialSteps[currentStep];

        QuestManager.Instance.Progress(target);

        bool isLastStep = currentStep == tutorialSteps.Length - 1;

        if (!isLastStep &&
            currentStep < afterCraftDialogues.Length &&
            afterCraftDialogues[currentStep] != null)
        {
            dialogueManager.StartDialogue(afterCraftDialogues[currentStep]);
        }


        currentStep++;

        if (currentStep >= tutorialSteps.Length)
        {
            FinishTutorial();
        }
        else
        {
            ShowInstruction();
        }
    }

}
