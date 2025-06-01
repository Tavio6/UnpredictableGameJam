using UnityEngine;

public class Gun : Weapon
{
    public Transform firePoint;

    protected override void Attack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        // Calculate rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Spawn and rotate bullet
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool("Player Bullet", firePoint.position, rotation);

        // Send direction to bullet
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

}