using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    public Collider2D attackCollider;
    private bool isAttacking = false;

    void Update()
    {
        RotateTowardsMouse();

        if (Input.GetMouseButton(0) && !isAttacking)
        {
            StartCoroutine(ContinuousAttack());
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private IEnumerator ContinuousAttack()
    {
        isAttacking = true;

        while (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", true);
            attackCollider.enabled = true;
            yield return new WaitForSeconds(1.30f);
            animator.SetBool("Attack", false);
            attackCollider.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false;
    }
}
