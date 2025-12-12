using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class TabToSpaces : MonoBehaviour
{
    TMP_InputField input;

    void Awake()
    {
        input = GetComponent<TMP_InputField>();
    }

    void OnGUI()
    {
        if (!input.isFocused) return;

        Event e = Event.current;

        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab)
        {
            e.Use(); // BLOKUJEMY domyœlne dzia³anie Unity
            InsertSpaces(4);
        }
    }

    void InsertSpaces(int count)
    {
        string spaces = new string(' ', count);
        int pos = input.stringPosition;

        input.text = input.text.Insert(pos, spaces);

        input.stringPosition = pos + count;
        input.caretPosition = pos + count;
    }
}
