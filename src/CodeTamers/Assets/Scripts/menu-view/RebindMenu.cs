using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // jeśli używasz TextMeshPro, jeśli zwykły Text -> using UnityEngine.UI;

public class RebindMenu : MonoBehaviour
{
    [Header("Input Action")]
    public InputActionReference moveAction;

    [Header("UI Texts")]
    public TextMeshProUGUI upText;
    public TextMeshProUGUI downText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    [Header("Binding IDs (skopiuj z Input Actions)")]
    public string upBindingId;
    public string downBindingId;
    public string leftBindingId;
    public string rightBindingId;

    // PUBLIC - przypisz w przyciskach UI
    public void RebindUp() => StartRebind(upBindingId);
    public void RebindDown() => StartRebind(downBindingId);
    public void RebindLeft() => StartRebind(leftBindingId);
    public void RebindRight() => StartRebind(rightBindingId);

    private void StartRebind(string bindingId)
    {
        if (moveAction == null)
        {
            Debug.LogError("Move Action nie przypisana!");
            return;
        }

        moveAction.action.Disable();

        int bindingIndex = GetBindingIndexById(bindingId);
        if (bindingIndex < 0)
        {
            Debug.LogError("Binding nie znaleziony! Sprawdź bindingId w Inspectorze.");
            moveAction.action.Enable();
            return;
        }

        moveAction.action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .OnComplete(op =>
            {
                op.Dispose();
                SaveBindings();
                moveAction.action.Enable();
                UpdateUI();
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            })
            .Start();
    }

    // Pobiera indeks bindingu po jego ID w ReadOnlyArray
    private int GetBindingIndexById(string bindingId)
    {
        var bindings = moveAction.action.bindings;
        for (int i = 0; i < bindings.Count; i++)
        {
            if (bindings[i].id.ToString() == bindingId)
                return i;
        }
        return -1;
    }

    // Zapis do PlayerPrefs
    private void SaveBindings()
    {
        PlayerPrefs.SetString("bindings",
            moveAction.action.actionMap.asset.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }

    // Wczytanie zapisanych bindingów
    public void LoadBindings()
    {
        if (PlayerPrefs.HasKey("bindings"))
        {
            moveAction.action.actionMap.asset
                .LoadBindingOverridesFromJson(PlayerPrefs.GetString("bindings"));
        }
        UpdateUI();
    }

    // Reset do domyślnych
    private void ResetBindings()
    {
        moveAction.action.actionMap.asset.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey("bindings");
        UpdateUI();
    }

    // PUBLIC do przycisku Reset
    public void OnResetButtonPressed()
    {
        ResetBindings();
        SaveBindings();
    }

    private void OnEnable()
    {
        LoadBindings();
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    // Aktualizacja UI
    private void UpdateUI()
    {
        if (moveAction == null) return;

        upText.text = "UP - " + GetReadableBinding(upBindingId);
        downText.text = "DOWN - " + GetReadableBinding(downBindingId);
        leftText.text = "LEFT - " + GetReadableBinding(leftBindingId);
        rightText.text = "RIGHT - " + GetReadableBinding(rightBindingId);
    }

    // Czytelna nazwa klawisza
    private string GetReadableBinding(string bindingId)
    {
        int index = GetBindingIndexById(bindingId);
        if (index < 0) return "None";

        var binding = moveAction.action.bindings[index];
        string path = string.IsNullOrEmpty(binding.overridePath) ? binding.path : binding.overridePath;

        // Klawiatura
        if (path.StartsWith("<Keyboard>/"))
        {
            string key = path.Substring(11); // usuwa "<Keyboard>/"
            key = key.Replace("ArrowUp", "UP")
                     .Replace("ArrowDown", "DOWN")
                     .Replace("ArrowLeft", "LEFT")
                     .Replace("ArrowRight", "RIGHT");
            return key.ToUpper();
        }

        // Gamepad / inne urządzenia
        if (path.StartsWith("<Gamepad>/"))
        {
            string key = path.Substring(9); // usuwa "<Gamepad>/"
            key = key.Replace("dpad/", "DPAD ").ToUpper();
            return key;
        }

        return path.ToUpper();
    }
}
