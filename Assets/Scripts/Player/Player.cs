using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Manager;
using Utility.ScriptableObject;

public abstract class Player : MonoBehaviour
{
    public int playerNumber;

    [SerializeField] SpriteRenderer itemDescribeRenderer;
    [SerializeField] protected Transform playerThrowTsf;
    [SerializeField] protected float pickRange;
    
    [SerializeField] protected float invincibleTime;

    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float stackedDamage;
    
    public float skillCooldown;
    private bool _canUseSkill = true;

    [Header("Hit Setting")]
    [SerializeField] float hitEffectDur = 0.5f;
    [SerializeField] AnimationCurve hitEffectCurve;
    
    [Header("Dont Touch")]
    [SerializeField] private int isInvincible;
    [SerializeField] protected ItemObj currentItem;
    
    [SerializeField] public InputController inputController;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected AnimationAdaptor playerAnimation;

    public float ultimateGauge;

    [SerializeField, Space(10f)]
    float deadLine_Y = -20f;
    
    SpriteRenderer spriteRenderer;
    private static readonly int IS_OUTLINE = Shader.PropertyToID("_isOutline");

    public int GetStackedDamage()
    {
        return (int)(stackedDamage);
    }
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<AnimationAdaptor>();

        if (itemDescribeRenderer) itemDescribeRenderer.sprite = null;
    }

    public void Heal(float healAmount)
    {
        stackedDamage -= healAmount;
        if (stackedDamage < 0) stackedDamage = 0;
    }

    public void TakeDamage(float damage, Vector2 knockbackDir = default, float knockbackPower = 0)
    {
        if(isInvincible > 0) return;
        
        SoundManager.Instance.PlaySFX("hit");

        StopCoroutine(nameof(HitEffect));
        StartCoroutine(nameof(HitEffect));

        stackedDamage += damage;

        ultimateGauge += damage * 0.5f * 0.01f;
        playerMovement.Knockback(knockbackDir, knockbackPower * (1 + stackedDamage * 0.01f));
        playerAnimation.GetHit(knockbackPower >= 10 ? 0.5f : 0.1f, knockbackPower);
        
        Invincible(invincibleTime);
    }

    IEnumerator HitEffect()
    {
        float _time = 0f;
        while(_time < hitEffectDur)
        {
            spriteRenderer.material.SetFloat("_HitValue", hitEffectCurve.Evaluate(_time / hitEffectDur));
            _time += Time.deltaTime;

            yield return null;
        }

        yield break;
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
            if(item.PickItem(this) ==ItemType.Equip)
                if (itemDescribeRenderer) itemDescribeRenderer.sprite = item.MainSprite;

            SoundManager.Instance.PlaySFX("itemPickup");
            break;
        }
    }

    private void Use()
    {
        if (currentItem == null) return;
        currentItem.TryUse(this, out bool isDestroyed);
        ultimateGauge += 0.02f;

        if (isDestroyed)
        {
            if (itemDescribeRenderer) itemDescribeRenderer.sprite = null;
            currentItem = null;
        }
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
        if (transform.position.y <= deadLine_Y || Mathf.Abs(transform.position.x) >= 50f)
        {
            Die();
            return;
        }
        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) ultimateGauge = 1f;
#endif

        if (inputController.GetKeyDown("Pick")) Pick();
        if (inputController.GetKeyDown("Attack") && currentItem == null) Attack();
        if (inputController.GetKeyDown("Attack") && currentItem != null) Use();
        if (inputController.GetKeyDown("Skill") && _canUseSkill)
        {
            Skill();
            StartCoroutine(SkillCoolDownCoroutine());
        }
        if (inputController.GetKeyDown("Ultimate") && ultimateGauge >= 1f)
        {
            Ultimate();
            ultimateGauge = 0;
            GameManager.Instance.UltimateEffectStart(playerNumber);
        }
    }

    private IEnumerator SkillCoolDownCoroutine()
    {
        _canUseSkill = false;
        spriteRenderer.material.SetFloat(IS_OUTLINE, 0f);

        yield return new WaitForSeconds(skillCooldown);
        _canUseSkill = true;
        spriteRenderer.material.SetFloat(IS_OUTLINE, 1f);
    }

    private void Die()
    {
        GameManager.Instance.Dead(playerNumber);
        
        Destroy(gameObject);
    }

    public void Invincible(float time)
    {
        StartCoroutine(InvincibleCoroutine(time));
    }
    
    private IEnumerator InvincibleCoroutine(float time)
    {
        isInvincible++;
        yield return new WaitForSeconds(time);
        isInvincible--;
    }

    protected abstract void Attack();
    protected abstract void Skill();
    protected abstract void Ultimate();
}
