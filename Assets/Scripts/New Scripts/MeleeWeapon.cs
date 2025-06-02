using System;
using System.Collections;
using UnityEngine;

public class Knife : Weapon
{
    public Animator spriteAnimator;
    public BoxCollider2D hitBox;
    public LayerMask enemyLayers;
    public int damage = 10;

    private void Awake()
    {
        RotationOffset = 270f;
        hitBox.enabled = false;
    }

    protected override void Attack()
    {
        StartCoroutine(KnifeSlashCo());
    }

    private IEnumerator KnifeSlashCo()
    {
        spriteAnimator.SetBool("Slashing", true);
        yield return new WaitForSeconds(0.2f);

        hitBox.enabled = true;
        yield return new WaitForSeconds(0.1f);
        hitBox.enabled = false;

        spriteAnimator.SetBool("Slashing", false);
    }
}