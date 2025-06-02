using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public WeaponManager weaponManager;
    
    [Header("Dash")]
    public float dashForce = 10f;
    public float dashCooldown = 1f;

    private float lastDashTime;


    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private string currentAnimation;

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
        
        // Choose animation
        if (animator !=null && moveInput != Vector2.zero)
        {
            if (Mathf.Abs(moveInput.x) > 0)
            {
                PlayAnimation("RunSideways");
            }
            else if (moveInput.y > 0)
            {
                PlayAnimation("RunBack");
            }
            else if (moveInput.y < 0)
            {
                PlayAnimation("RunFront");
            }
        }
        else
        {
            //PlayAnimation("Idle"); // Optional: replace with your idle animation state name
        }
        
        // dash if player's holding knife
        if (Input.GetMouseButtonDown(1) && weaponManager.IsHoldingKnife() && Time.time >= lastDashTime + dashCooldown)
        {
            DashTowardsMouse();
        }

    }

    void DashTowardsMouse()
    {
        lastDashTime = Time.time;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position);
        direction.Normalize();

        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
    }

    
    void FixedUpdate()
    {
        // Smooth movement
        Vector2 targetVelocity = moveInput * moveSpeed;
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref currentVelocity, smoothTime);
    }
    
    void PlayAnimation(string animationName)
    {
        if (currentAnimation == animationName) return;
        animator.Play(animationName);
        currentAnimation = animationName;
    }
}