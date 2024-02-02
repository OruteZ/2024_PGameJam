using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ScriptableObject;

public class P1 : Player
{
    [Header("Attack")]
    public float attackRange;
    public Transform attackTsf;
    public float attackDamage;
    public Vector2 attackKnockbackDir;
    public float attackKnockbackPower;
    
    [Header("Skill")]
    public float skillRange;
    public float skillDuration;
    public float skillDamage;
    public Vector2 skillKnockbackDir;
    public float skillKnockbackPower;
    
    
    protected override void Attack()
    {
        //attack
        var colliders = Physics2D.OverlapCircleAll(attackTsf.position, attackRange);
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<Player>();
            if (enemy == null || enemy == this) continue;
            enemy.TakeDamage(attackDamage, attackKnockbackDir, attackKnockbackPower);
        }
    }

    private bool _isUsingSkill;

    protected override void Skill()
    {
        _isUsingSkill = true;
        StartCoroutine(SkillCoroutine());
    }

    

    protected override void Ultimate()
    {
        throw new System.NotImplementedException();
    }
    
    //draw gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, pickRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackTsf.position, attackRange);
    }
    
    private IEnumerator SkillCoroutine()
    {
        inputController.canInput = false;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = originalPosition + GetFacing() * skillRange;
        float timeElapsed = 0;

        while (timeElapsed < skillDuration)
        {
            transform.position = Vector2.Lerp(originalPosition, targetPosition, timeElapsed / skillDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        _isUsingSkill = false;
        
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        inputController.canInput = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isUsingSkill)
        {
            var enemy = collision.collider.GetComponent<Player>();
            if (enemy != null && enemy != this)
            {
                enemy.TakeDamage(skillDamage, skillKnockbackDir, skillKnockbackPower);
            }
        }
    }
}
