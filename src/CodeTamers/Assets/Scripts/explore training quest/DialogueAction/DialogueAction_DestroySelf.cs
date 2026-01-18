using UnityEngine;

public class DialogueAction_DestroySelf : MonoBehaviour, IDialogueAction
{
    public bool destroyRoot = true; // czy niszczyæ ca³y obiekt (polecane)

    public void Execute(System.Action onFinished)
    {
        if (destroyRoot)
            Destroy(transform.root.gameObject);
        else
            Destroy(gameObject);

        onFinished?.Invoke();
    }
}
