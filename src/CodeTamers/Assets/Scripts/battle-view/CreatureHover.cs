using UnityEngine;

public class CreatureHover : MonoBehaviour
{
    SpriteRenderer sr;
    Color originalColor;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;
    }

    void OnMouseEnter()
    {
        if (sr != null)
            sr.color = new Color(1f, 1f, 0.6f); // soft yellow highlight
    }

    void OnMouseExit()
    {
        if (sr != null)
            sr.color = originalColor;
    }
}
