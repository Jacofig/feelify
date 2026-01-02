using UnityEngine;

public class OpenWorldCommandHandler : MonoBehaviour, IGameCommandHandler
{
    public PlayerSpeechBubble playerBubble;

    public bool CanExecute(string commandName)
    {
        return true; // w open worldzie zawsze mo¿na
    }

    public bool ExecuteCommand(string commandName, string[] args)
    {
        switch (commandName)
        {
            case "attack":
                playerBubble.ShowBubble("Atakujê!");
                return true;

            case "block":
                playerBubble.ShowBubble("Blokujê!");
                return true;

            default:
                playerBubble.ShowBubble("Nieznana komenda");
                return false;
        }
    }
}
