using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 10;
    public string damageLayer;
    
    private Vector2 direction;
    private float timer;
    
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        timer = 0f;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            DestoryBullet();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer(damageLayer))
        {
            collision.GetComponent<Health>()?.TakeDamage(damage);
            DestoryBullet();
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            DestoryBullet();
        }
    }

    void DestoryBullet()
    {
        // Return bullet to pool
        ObjectPooler.Instance.ReturnToPool(gameObject.name, gameObject);
    }
}