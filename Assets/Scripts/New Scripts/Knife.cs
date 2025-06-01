using System;
using System.Collections;
using UnityEngine;

public class Knife : Weapon
{
    
    public Animator spriteAnimator;
    public BoxCollider2D hitBox;
    
    public LayerMask enemyLayers;
    private void Awake()
    {
        RotationOffset = 270f; // place the knife at the opposite side of the mouse. only bring to the mouse side when slashing
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
        spriteAnimator.SetBool("Slashing", false);
    }
}