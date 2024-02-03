﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ScriptableObject;

public abstract class Player : MonoBehaviour
{
    public int playerNumber;
    
    [SerializeField] protected Transform playerThrowTsf;
    [SerializeField] protected float pickRange;
    
    [SerializeField] protected float invincibleTime;

    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float stackedDamage;
    
    [Header("Dont Touch")]
    [SerializeField] private int isInvincible;
    [SerializeField] protected ItemObj currentItem;
    
    [SerializeField] public InputController inputController;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected AnimationAdaptor playerAnimation;

    public float ultimateGauge;
    
    public int GetStackedDamage()
    {
        return (int)(stackedDamage);
    }
    
    private void Awake()
    {
        
        playerAnimation = GetComponent<AnimationAdaptor>();
    }

    public void Heal(float healAmount)
    {
        stackedDamage -= healAmount;
        if (stackedDamage < 0) stackedDamage = 0;
    }

    public void TakeDamage(float damage, Vector2 knockbackDir = default, float knockbackPower = 0)
    {
        if(isInvincible > 0) return;
        
        stackedDamage += damage;

        ultimateGauge += damage * 0.2f * 0.01f;
        playerMovement.Knockback(knockbackDir, knockbackPower * (1 + stackedDamage * 0.01f));
        playerAnimation.GetHit(knockbackPower >= 10 ? 0.5f : 0.1f, knockbackPower);
        
        Invincible();
    }
    
    public Vector2 GetFacing()
    {
        return playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
    }

    private void Pick()
    {
        //if there is no item, pick item
        if (currentItem != null) return;
        
        var colliders = Physics2D.OverlapCircleAll(playerThrowTsf.position, pickRange);
        foreach (var collider in colliders)
        {
            var item = collider.GetComponent<ItemObj>();
            if (item == null) continue;

            currentItem = item;
            item.PickItem(this);
            break;
        }
    }

    private void Use()
    {
        if (currentItem == null) return;
        currentItem.TryUse(this, out bool isDestroyed);
        if (isDestroyed) currentItem = null;
    }

    public void SudoUnEquip()
    {
        currentItem = null;
    }
    
    public Transform GetPlayerThrowTsf()
    {
        return playerThrowTsf;
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) ultimateGauge = 1f;
#endif

        if (inputController.GetKeyDown("Pick")) Pick();
        if (inputController.GetKeyDown("Attack") && currentItem == null) Attack();
        if (inputController.GetKeyDown("Attack") && currentItem != null) Use();
        if (inputController.GetKeyDown("Skill")) Skill();
        if (inputController.GetKeyDown("Ultimate") && ultimateGauge >= 1f)
        {
            Ultimate();
            ultimateGauge = 0;
        }
    }

    private void Die()
    {
        
    }

    private void Invincible()
    {
        StartCoroutine(InvincibleCoroutine());
    }
    
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible++;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible--;
    }

    protected abstract void Attack();
    protected abstract void Skill();
    protected abstract void Ultimate();
}
