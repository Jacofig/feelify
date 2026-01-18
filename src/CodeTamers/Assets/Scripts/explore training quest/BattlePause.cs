using UnityEngine;

public class BattlePause : MonoBehaviour
{
    public static void SetMovementActive(bool active)
    {
        // Zablokuj / odblokuj ruch gracza
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
            player.enabled = active;

        // Zablokuj / odblokuj ruch wszystkich NPC
        patrol[] npcs = FindObjectsOfType<patrol>();
        foreach (var npc in npcs)
            npc.enabled = active;

        chase[] chasers = FindObjectsOfType<chase>();
        foreach (var c in chasers)
            c.enabled = active;
    }
}
