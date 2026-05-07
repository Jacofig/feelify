using UnityEngine;

public class DialogueAction_DisableSelf : MonoBehaviour, IDialogueAction
{
    public bool disableRoot = true; // czy wy³¹czyæ ca³y obiekt

    public void Execute(System.Action onFinished)
    {
        if (disableRoot)
            transform.root.gameObject.SetActive(false);
        else
            gameObject.SetActive(false);

        onFinished?.Invoke();
    }
}