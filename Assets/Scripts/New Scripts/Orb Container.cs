using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrbContainer : MonoBehaviour
{
    [Header("Orb Container")]
    public List<Orb> possibleOrbs = new List<Orb>();
    public OrbDisplayUI orbDisplayUI;

    [Header("References")] 
    public WeaponManager WeaponManager;
    
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
                    WeaponManager.SelectWeapon(WeaponType.Knife);
                }
            ),
            new (
                "Orb of Cowardice",
                "Replaces your medium-range whip with a long-range gun. Enemies are smaller and faster.",
                Resources.Load<Sprite>("Sprites/Orbs/Cowardice"),
                () =>
                {
                    WeaponManager.SelectWeapon(WeaponType.Knife);
                }
            ),
            new (
                "Orb of Comeuppance",
                "You can now attack an enemy's attack to parry it, which allows said attack to hurt enemies. Enemy attacks are faster and larger.",
                Resources.Load<Sprite>("Sprites/Orbs/Comeuppance"),
                () =>
                {
                    WeaponManager.SelectWeapon(WeaponType.Knife);
                }
            )
        });
    }
}