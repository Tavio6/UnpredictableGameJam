using UnityEngine;
using UnityEngine.Serialization;

public class MeleeHitbox : MonoBehaviour
{
    [FormerlySerializedAs("parentKnife")] public MeleeWeapon parentMeleeWeapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(parentMeleeWeapon.damage);
        }
        else if (GameManager.Instance.CanParryBullets && collision.TryGetComponent<Parry>(out var parry))
        {
            parry.TakeDamage(parentMeleeWeapon.damage);
        }
    }
}