using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private Vector2 direction;
    private float timer;

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
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // collision.GetComponent<Enemy>()?.TakeDamage();
            gameObject.SetActive(false);
        }
    }
}