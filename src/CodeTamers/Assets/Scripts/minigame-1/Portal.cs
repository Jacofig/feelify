using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public Color portalColor = Color.white;

    [HideInInspector]
    public bool canTeleport = true;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = portalColor;

        if (linkedPortal != null)
        {
            SpriteRenderer srLinked = linkedPortal.GetComponent<SpriteRenderer>();
            if (srLinked != null)
                srLinked.color = portalColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            other.transform.position = linkedPortal.transform.position;

            // blokada obu portali do czasu kolejnego obrotu
            canTeleport = false;
            if (linkedPortal != null)
                linkedPortal.canTeleport = false;
        }
    }
}