using UnityEngine; // <- TO JEST KONIECZNE

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string text;            // Tekst linii
        public Speaker speaker;        // Kto mówi: gracz czy NPC
        public string speakerName;     // np. "Riven" albo "AI"
        public Sprite speakerIcon;     // Ikona mówi¹cego w UI
    }

    public DialogueLine[] lines;       // Tablica linii dialogu
}

public enum Speaker
{
    Player,
    NPC
}
