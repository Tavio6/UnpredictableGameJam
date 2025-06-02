using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController2D : MonoBehaviour
{
    public Transform weaponHolder;
    public Transform weapon;
    
    public float fireRate = 1f;
    private float fireCooldown;
    
    public LayerMask obstacleMask; // Set to "Wall" layer
    public LayerMask playerMask;    // Set to "Player" layer
    public float visionDistance = 10f;
    
    public float runSpeed = 3f;
    public float detectionDistance = 2f;

    private Transform player;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        
        RotateWeaponTowardsMouse();
        
        Vector2 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        bool canSeePlayer = CanSeePlayer(directionToPlayer.normalized);

        if (canSeePlayer)
        {
            // Shoot
            if (Time.time >= fireCooldown)
            {
                Shoot(directionToPlayer.normalized);
                fireCooldown = Time.time + 1f / fireRate;
            }
        }
    }
    
    private void RotateWeaponTowardsMouse()
    {
        Vector2 direction = (player.position - weaponHolder.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        weaponHolder.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public bool CanSeePlayer(Vector2 directionToPlayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionDistance, obstacleMask | playerMask);
        Debug.DrawRay(transform.position, directionToPlayer * visionDistance, Color.red);

        if (hit.collider != null)
        {
            // Check if we hit the player directly
            if (((1 << hit.collider.gameObject.layer) & playerMask) != 0)
            {
                return true;
            }
        }

        return false;
    }

    void Shoot(Vector2 direction)
    { 
        // Spawn and rotate bullet
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("Enemy Bullet", weapon.position, Quaternion.identity);

        // Send direction to bullet
        bullet.GetComponent<Bullet>().SetDirection(direction.normalized);
    }
}
