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
        
        //create set collection
        HashSet<Player> attackedPlayers = new HashSet<Player>();
        
        float time = 0;
        while (time < attackCooldown)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackTsf.position, attackRange);
            foreach (var collider in colliders)
            {
                Debug.Log(collider.name);
                var enemy = collider.GetComponent<Player>();
                if (enemy != null && enemy != this && !attackedPlayers.Contains(enemy))
                {
                    ultimateGauge += 0.1f * attackDamage * 0.01f;
                    
                    enemy.TakeDamage(attackDamage, attackKnockbackDir * direction, attackKnockbackPower);
                    attackedPlayers.Add(enemy);
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
        
        
        // playerMovement.SetConstState(false);
        inputController.canInput = true;
    }
    
    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
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
