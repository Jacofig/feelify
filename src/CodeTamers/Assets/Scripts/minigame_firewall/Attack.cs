using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 2f;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // wirus przelecia³ przez mapê
        if (Mathf.Abs(transform.position.x) > 5 ||
            Mathf.Abs(transform.position.y) > 3)
        {
            GameManager.instance.VirusPassed();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControllerF player = other.GetComponent<PlayerControllerF>();

        if (player != null)
        {
            // z³apany wirus
            Destroy(gameObject);
        }
    }
}