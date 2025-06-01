using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 1.5f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(rb.position, player.position);

            if (distanceToPlayer > stoppingDistance)
            {
                Vector2 direction = ((Vector2)player.position - rb.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
