using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public Transform weaponHolder;
    public Transform weapon;
    
    public LayerMask obstacleMask; // Set to "Wall" layer
    public LayerMask playerMask;   // Set to "Player" layer
    public float visionDistance = 10f;

    private Transform player;
    private Rigidbody2D rb;

    [Header("Strafe Settings")]
    public bool moveX = true;
    public float strafeOffset = 1f;
    public float strafeInterval = 4f;
    public float strafeSpeed = 2f;
    
    private float strafeCooldown = 0f;
    private bool isStrafing = false;
    
    [Header("Stats")]
    public int bulletDamage = 10;
    public float fireRate = 1f;
    
    private float fireCooldown;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        RotateWeaponTowardsMouse();

        Vector2 directionToPlayer = player.position - transform.position;

        bool canSeePlayer = CanSeePlayer(directionToPlayer.normalized);

        if (canSeePlayer)
        {
            if (Time.time >= fireCooldown)
            {
                Shoot(directionToPlayer.normalized);
                fireCooldown = Time.time + 1f / fireRate;
            }

            if (Time.time >= strafeCooldown && !isStrafing)
            {
                StartStrafeSequence();
                strafeCooldown = Time.time + strafeInterval;
            }
        }
    }

    private void RotateWeaponTowardsMouse()
    {
        Vector2 direction = (player.position - weaponHolder.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponHolder.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public bool CanSeePlayer(Vector2 directionToPlayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionDistance, obstacleMask | playerMask);
        Debug.DrawRay(transform.position, directionToPlayer * visionDistance, Color.red);

        if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & playerMask) != 0)
        {
            return true;
        }

        return false;
    }

    void Shoot(Vector2 direction)
    {
        GameObject bulletObj = ObjectPooler.Instance.SpawnFromPool("Enemy Bullet", weapon.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetDirection(direction.normalized);
        bullet.damage = bulletDamage;
    }

    void StartStrafeSequence()
    {
        isStrafing = true;

        Vector2 offset = moveX
            ? new Vector2(strafeOffset, 0f)
            : new Vector2(0f, strafeOffset);
        
        Vector2 p0 = rb.position;
        Vector2 p1 = p0 + offset;
        Vector2 p2 = p0 - offset;

        // Temporary tween variable
        Vector2 tweenPosition = p0;

        DOTween.Sequence()
            .Append(DOTween.To(() => tweenPosition, x => {
                tweenPosition = x;
                rb.MovePosition(x);
            }, p1, 1/strafeSpeed))
            .Append(DOTween.To(() => tweenPosition, x => {
                tweenPosition = x;
                rb.MovePosition(x);
            }, p2, 1/strafeSpeed))
            .Append(DOTween.To(() => tweenPosition, x => {
                tweenPosition = x;
                rb.MovePosition(x);
            }, p0, 1/strafeSpeed))
            .OnComplete(() => isStrafing = false);
    }
}
