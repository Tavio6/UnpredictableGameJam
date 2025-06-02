using UnityEngine;
using UnityEngine.Serialization;

public class MeleeHitbox : MonoBehaviour
{
    [FormerlySerializedAs("parentKnife")] public MeleeWeapon parentMeleeWeapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & parentMeleeWeapon.enemyLayers) != 0)
        {
            collision.GetComponent<Health>()?.TakeDamage(parentMeleeWeapon.damage);
        }
    }
}