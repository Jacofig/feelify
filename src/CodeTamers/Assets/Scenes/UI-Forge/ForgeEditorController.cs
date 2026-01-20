using UnityEngine;
using TMPro;

public class ForgeEditorController : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputField;

    [Header("Forge")]
    public ForgeManager forgeManager;

    private string codeBuffer = "";

    void Awake()
    {
        if (inputField != null)
            inputField.onValueChanged.AddListener(OnCodeChanged);
    }

    void OnDestroy()
    {
        if (inputField != null)
            inputField.onValueChanged.RemoveListener(OnCodeChanged);
    }

    private void OnCodeChanged(string value)
    {
        codeBuffer = value;
    }

    public void RunForge()
    {
        if (forgeManager == null)
        {
            Debug.LogError("ForgeManager not assigned!");
            return;
        }

        if (forgeManager.activeRecipe == null)
        {
            Debug.Log("No active recipe selected!");
            return;
        }

        if (inputField == null)
        {
            Debug.LogError("InputField not assigned!");
            return;
        }

        string code = inputField.text;

        Debug.Log("RUN CODE:\n" + code);

        forgeManager.RunForge(code);
    }

}
