using UnityEngine;

public class OpenWorldCommandHandler : MonoBehaviour, IGameCommandHandler
{
    public PlayerSpeechBubble playerBubble;

    public void ExecuteCommand(string commandName)
    {
        switch (commandName)
        {
            case "attack":
                playerBubble.ShowBubble("Atakujê!");
                break;

            case "block":
                playerBubble.ShowBubble("Blokujê!");
                break;

            default:
                playerBubble.ShowBubble("Nieznana komenda");
                break;
        }
    }
}
