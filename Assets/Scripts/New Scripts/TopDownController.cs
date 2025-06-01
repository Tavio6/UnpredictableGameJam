using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Get input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Flip character sprite based on horizontal movement
        if (moveInput.x != 0)
            spriteRenderer.flipX = moveInput.x < 0;
    }

    void FixedUpdate()
    {
        // Smooth movement
        Vector2 targetVelocity = moveInput * moveSpeed;
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref currentVelocity, smoothTime);
    }
}