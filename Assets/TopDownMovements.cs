using UnityEngine;

public class TopDownMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once per frame
    void Update()
    {
        // Get player input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate is called at a fixed interval and is best for physics-based movement
    void FixedUpdate()
    {
        // Move the Rigidbody2D using velocity
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
