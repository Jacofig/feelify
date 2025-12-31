using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BattleEditorController : MonoBehaviour
{
    public TMP_InputField inputField;

    private List<Creature> creatures;
    private Creature activeCreature;

    public void Init(List<Creature> playerCreatures)
    {
        creatures = playerCreatures;

        // Select first creature by default
        SelectCreature(0);

        // Editor should be visible
        gameObject.SetActive(true);
    }

    public void SelectCreature(int index)
    {
        if (index < 0 || index >= creatures.Count)
            return;

        // Save previous code
        if (activeCreature != null)
            activeCreature.codeBuffer = inputField.text;

        // Switch active creature
        activeCreature = creatures[index];

        // Load its code
        inputField.text = activeCreature.codeBuffer;
    }

    public Creature GetActiveCreature()
    {
        return activeCreature;
    }
}
