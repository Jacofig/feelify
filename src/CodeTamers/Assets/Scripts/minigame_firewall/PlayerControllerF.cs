using UnityEngine;

public class PlayerControllerF : MonoBehaviour
{
    public float speed = 6f;
    public int hp = 5;

    Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        Debug.Log("HP: " + hp);

        if (hp <= 0)
        {
            GameManager.instance.LoseGame();
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        float x = Mathf.Clamp(transform.position.x, -3.5f, 3.5f);
        float y = Mathf.Clamp(transform.position.y, -1.8f, 1.8f);

        transform.position = new Vector3(x, y, 0);
    }
}