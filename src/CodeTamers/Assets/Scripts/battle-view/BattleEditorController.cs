using UnityEngine;
using TMPro;
using System;

public class BattleEditorController : MonoBehaviour
{
    public TMP_InputField inputField;

    private Creature boundCreature;
    private BattleManager battleManager;

    private bool isOpen;

    public void Init(BattleManager manager)
    {
        battleManager = manager;
        CloseEditor(); // start closed
    }

    // Switch which pokemon is using the editor right now
    public void BindCreature(Creature creature)
    {
        boundCreature = creature;

        // load that creature's code
        inputField.text = boundCreature != null ? boundCreature.codeBuffer : "";
    }

    public void OpenEditor()
    {
        gameObject.SetActive(true);
        isOpen = true;
        inputField.ActivateInputField();
    }

    public void CloseEditor()
    {
        // save buffer before closing
        if (boundCreature != null)
            boundCreature.codeBuffer = inputField.text;

        inputField.text = "";
        isOpen = false;
        gameObject.SetActive(false);
    }

    public void SubmitCode()
    {
        if (boundCreature == null)
        {
            Debug.LogError("No creature bound to editor!");
            return;
        }

        // save buffer
        boundCreature.codeBuffer = inputField.text;

        try
        {
            battleManager.ExecuteCreatureCode(boundCreature, boundCreature.codeBuffer);
        }
        catch (Exception ex)
        {
            Debug.LogError("Execution error: " + ex.Message);
        }
    }

    public bool IsOpen() => isOpen;
}
