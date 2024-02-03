﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ScriptableObject;

public class P2 : Player
{
    [Header("Attack")]
    public Transform attackTsf;
    public GameObject attackObj;

    [Header("Skill")] 
    public GameObject mine;
    public Transform mineSpawnTsf;
    
    [Header("Ultimate")]
    public float ultimateDuration;
    public Rect ultimateSpawnRect;
    public Transform ultimateSpawnPosition;
    public float ultimateDamage;
    public float ultimateKnockbackPower;
    public GameObject ultimateEffect;

    private Vector2 direction => playerMovement.IsFacingRight ? new Vector2(1, 1) : new Vector2(-1, 1);
    
    
    private bool _canAttack = true;
    protected override void Attack()
    {
        if (!_canAttack) return;
        
        playerAnimation.OnAttack();
        Debug.Log("Attack!");
        
        //attack : check colliders while 0.5 seconds
        StartCoroutine(AttackCoroutine());
        
        //cooldown
        StartCoroutine(AttackCooldown());
    }
    
    private IEnumerator AttackCoroutine()
    {
        //set const movement
        // playerMovement.SetConstState(true);
        inputController.canInput = false;

        yield return new WaitForSeconds(0.1f);
        
        //throw attack object
        var attackObjInstance = Instantiate(attackObj, attackTsf.position, Quaternion.identity);
        attackObjInstance.GetComponent<ArtThrowObj>().SetOwner(this, playerMovement.IsFacingRight ? 1 : -1);
        
        yield return new WaitForSeconds(0.2f);
        
        // playerMovement.SetConstState(false);
        inputController.canInput = true;
    }
    
    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }


    protected override void Skill()
    {
        if (playerMovement.LastOnGroundTime <= 0) return;
        
        //setup mine
        var mineObj = Instantiate(mine, mineSpawnTsf.position, Quaternion.identity);
        mineObj.GetComponent<MineSkill>().SetPlayer(this);
    }

    

    protected override void Ultimate()
    {
        StartCoroutine(UltimateCoroutine());
    }
    
    //draw gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, pickRange);
        
        //draw ultimate rect
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ultimateSpawnPosition.position, ultimateSpawnRect.size);
    }
    
    private IEnumerator UltimateCoroutine()
    {
        //set const movement
        // playerMovement.SetConstState(true);
        inputController.canInput = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        
        //show ultimate effect
        var ultimateEffectInstance = Instantiate(ultimateEffect, ultimateSpawnPosition.position, Quaternion.identity);
        
        
        float timer = 0;
        //while ultimate duration
        while (timer < ultimateDuration)
        {
            timer += Time.deltaTime;
            yield return null;
            //if last 0.5second, knockbackPower *= 10
            float knockbackPower = 
                ultimateDuration <= 0.5f ? ultimateKnockbackPower * 10 : ultimateKnockbackPower;
            
            var colliders = Physics2D.OverlapBoxAll(ultimateSpawnPosition.position, ultimateSpawnRect.size, 0);
            foreach (var collider in colliders)
            {
                var player = collider.GetComponent<Player>();
                if (player == null) continue;
                if (player == this) continue;
            
                player.TakeDamage(ultimateDamage, direction, knockbackPower);
            }
            
            yield return null;
        }
        
        
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        //remove ultimate effect
        Destroy(ultimateEffectInstance);
        
        // playerMovement.SetConstState(false);
        inputController.canInput = true;
    }
}