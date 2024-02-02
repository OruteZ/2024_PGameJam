using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ScriptableObject;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected InputController inputController;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected Transform playerThrowTsf;
    [SerializeField] protected ItemObj currentItem;
    [SerializeField] protected float pickRange;
    [SerializeField] protected float maxHp;
    
    [SerializeField] protected float invincibleTime;
    [SerializeField] private bool isInvincible;
    
    protected float currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void Heal(float healAmount)
    {
        currentHp = Mathf.Min(currentHp + healAmount, maxHp);
    }

    public void TakeDamage(float damage, Vector2 knockbackDir = default, float knockbackPower = 0)
    {
        if(isInvincible) return;
        
        currentHp -= damage;
        if (currentHp <= 0) Die();
        playerMovement.Knockback(knockbackDir, knockbackPower);
        
        Invincible();
    }
    
    public Vector2 GetFacing()
    {
        return playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
    }

    private void Pick()
    {
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
        if (inputController.GetKeyDown("Pick")) Pick();
        if (inputController.GetKeyDown("Attack") && currentItem == null) Attack();
        if (inputController.GetKeyDown("Attack") && currentItem != null) Use();
        if (inputController.GetKeyDown("Skill")) Skill();
        if (inputController.GetKeyDown("Ultimate")) Ultimate();
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
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }


    protected abstract void Attack();
    protected abstract void Skill();
    protected abstract void Ultimate();
}
