using UnityEngine;

public class Parry : MonoBehaviour
{
    public Health parentEnemyHealth;

    public void TakeDamage(int damage)
    {
        parentEnemyHealth.TakeDamage(damage);
    }
}
