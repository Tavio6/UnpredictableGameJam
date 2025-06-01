using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public float knockbackForce = 5f;

    [SerializeField] private Image hpFillImage; // Assign in Inspector

    void Start()
    {
        health = maxHealth;
        UpdateHPBar();
    }

    void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //ApplyKnockback();
        UpdateHPBar();
    }

    void ApplyKnockback()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHPBar()
    {
        if (hpFillImage != null)
        {
            hpFillImage.fillAmount = Mathf.Clamp01((float)health / maxHealth);
        }
    }
}