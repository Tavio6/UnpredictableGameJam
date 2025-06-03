using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class OrbContainer : MonoBehaviour
{
    [Header("Orb Container")]
    public List<Orb> possibleOrbs = new List<Orb>();
    public OrbDisplayUI orbDisplayUI;

    [Header("Orb of Bravery")] 
    public WeaponManager weaponManager;

    [Header("Orb of Cowardice")]
    public GameObject enemiesParent;
    public float strafeIntervalDelta = 0.5f;
    public float strafeSpeedDelta = 1f;

    [Header("Orb of Comeuppance")] 
    public int enemyDamageDelta = 5;
    public float enemyFireRateDelta = 1f;
    
    
    private bool hasGivenOrb = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasGivenOrb) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            hasGivenOrb = true;

            Orb randomOrb = possibleOrbs[Random.Range(0, possibleOrbs.Count)];

            if (orbDisplayUI != null)
                orbDisplayUI.ShowOrb(randomOrb);

            randomOrb.ApplyEffect();
        }
    }

    private void Awake()
    {
        possibleOrbs.AddRange(new List<Orb>
        {
            new (
                "Orb of Bravery",
                "Replaces your medium-range whip with a short-range knife. You can hold the attack button to dash before your attack.",
                Resources.Load<Sprite>("Sprites/Orbs/Bravery"),
                () =>
                {
                    weaponManager.SelectWeapon(WeaponType.Knife);
                }
            ),
            new (
                "Orb of Cowardice",
                "Replaces your medium-range whip with a long-range gun. Enemies are smaller and faster.",
                Resources.Load<Sprite>("Sprites/Orbs/Cowardice"),
                () =>
                {
                    weaponManager.SelectWeapon(WeaponType.Gun);
                    var allEnemies = enemiesParent.GetComponentsInChildren<EnemyController>(includeInactive: true);

                    foreach (var enemy in allEnemies)
                    {
                        enemy.strafeInterval -= strafeIntervalDelta;
                        enemy.strafeSpeed += strafeSpeedDelta;
                    }
                }
            ),
            new (
                "Orb of Comeuppance",
                "You can now attack an enemy's attack to parry it, which allows said attack to hurt enemies. Enemy attacks are faster and larger.",
                Resources.Load<Sprite>("Sprites/Orbs/Comeuppance"),
                () =>
                {
                    GameManager.Instance.CanParryBullets = true;
                    var allEnemies = enemiesParent.GetComponentsInChildren<EnemyController>(includeInactive: true);

                    foreach (var enemy in allEnemies)
                    {
                        enemy.bulletDamage += enemyDamageDelta;
                        enemy.fireRate += enemyFireRateDelta;
                    }
                }
            )
        });
    }
}