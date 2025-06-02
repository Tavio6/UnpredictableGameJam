using UnityEngine;

public enum WeaponType
{
    Whip,
    Knife,
    Gun
}


public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; // Make sure array is in the order of enum values
    private WeaponType _currentWeapon;

    public bool IsHoldingKnife()
    {
        return _currentWeapon == WeaponType.Knife;
    }

    void Start()
    {
        SelectWeapon(WeaponType.Whip);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cycle to next weapon
            int next = ((int)_currentWeapon + 1) % weapons.Length;
            SelectWeapon((WeaponType)next);
        }
    }

    public void SelectWeapon(WeaponType weaponType)
    {
        _currentWeapon = weaponType;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == (int)weaponType);
        }
    }
}