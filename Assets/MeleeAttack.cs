using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    private bool isAttacking = false; // Flag to track ongoing attack

    void Update()
    {
        RotateTowardsMouse();

        // Check if the left mouse button is being held down AND attack isn't already happening
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

        while (Input.GetMouseButton(0)) // Keep attacking while the button is held
        {
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(1.30f); // Wait for attack animation duration
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(0.1f); // Small delay before next attack
        }

        isAttacking = false;
    }
}
