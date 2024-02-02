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
    
    [Header("Ultimate")]
    public float ultimateDuration;
    public GameObject ultimateShooter;
    public Vector3 ultimateSpawnPosition;
    public float spawnOffset;
    public int ultimateDronesCount;

    private Vector2 direction => playerMovement.IsFacingRight ? new Vector2(1, 1) : new Vector2(-1, 1);
    
    
    protected override void Attack()
    {
        Debug.Log("Attack!");
        
        //attack
        var colliders = Physics2D.OverlapCircleAll(attackTsf.position, attackRange);
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<Player>();
            if (enemy == null || enemy == this) continue;
            enemy.TakeDamage(attackDamage, attackKnockbackDir * direction, attackKnockbackPower);
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
        StartCoroutine(UltimateCoroutine());
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
                enemy.TakeDamage(skillDamage, skillKnockbackDir * direction, skillKnockbackPower);
            }
        }
    }
    
    private IEnumerator UltimateCoroutine()
    {
        //spawn "ultimate spawn count" drones while ultimate duration
        for (int i = 0; i < ultimateDronesCount; i++)
        {
            //spawn drone, randomized y position : maximum spawnOffset
            var drone = Instantiate(
                ultimateShooter, 
                ultimateSpawnPosition + new Vector3(0, Random.Range(-spawnOffset, spawnOffset), 0), 
                Quaternion.identity
                );
            
            //set owner
            drone.GetComponent<Drone>().SetOwner(this);
            
            yield return new WaitForSeconds(ultimateDuration / ultimateDronesCount);
        }
    }
}
