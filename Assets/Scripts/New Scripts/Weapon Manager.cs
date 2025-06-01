using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 1;

    void Start()
    {
        SelectWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            SelectWeapon(currentWeaponIndex);
        }
    }

    void SelectWeapon(int index)
    {
        currentWeaponIndex = index;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }
    }
}