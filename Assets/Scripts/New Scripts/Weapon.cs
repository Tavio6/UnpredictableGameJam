using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float RotationOffset = 90f;
    protected virtual void Update()
    {
        RotateTowardsMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void RotateTowardsMouse()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle - RotationOffset);
    }

    protected abstract void Attack();
}