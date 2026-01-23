using TMPro;
using UnityEngine;

public class NameFollowNPC : MonoBehaviour
{
    public Transform npcRoot; // przeci¹gnij root NPC
    public Vector3 offset = new Vector3(0, 1.2f, 0); // nad g³ow¹

    public TextMeshPro text; // przeci¹gnij TMP

    void LateUpdate()
    {
        if (npcRoot != null && text != null)
        {
            transform.position = npcRoot.position + offset;
        }
    }
}
