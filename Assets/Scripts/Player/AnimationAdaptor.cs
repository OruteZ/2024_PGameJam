using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationAdaptor : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rigid;
    private Player _player;

    [SerializeField] private float knockbackTime;
    
    private static readonly string IS_MOVING = "XMoving";
    private static readonly string IS_ATTACKING = "Attack";
    private static readonly string IS_JUMPING = "JumpTrigger";
    private static readonly string IS_FALLING = "IsFalling";
    private static readonly string TAKE_DAMAGE = "TakeDamage";
    private static readonly string KNOCKBACK_POWER = "KnockbackPower";
    private static readonly string KNOCKBACK_TIME = "KnockbackTime";
    private static readonly string ON_GROUND = "OnGround";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rigid = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        
        _playerMovement.OnJump.AddListener(() => SetTrigger(IS_JUMPING));
    }

    private void Update()
    {
        knockbackTime = Mathf.Max(-1, knockbackTime - Time.deltaTime);
        SetFloat(KNOCKBACK_TIME, knockbackTime);
        
        SetBool(IS_MOVING, Mathf.Abs(_rigid.velocity.x) > 0.5f);
        SetBool(IS_FALLING, _rigid.velocity.y < -0.01f);
        SetBool(ON_GROUND, _playerMovement.LastOnGroundTime > 0);
    }
    
    public void SetBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }
    
    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }
    
    public void SetFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
    
    public void SetInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }

    public void OnAttack()
    {
        SetTrigger(IS_ATTACKING);
    }
    
    public void GetHit(float getDamageTime, float knockbackPower)
    {
        SetTrigger(TAKE_DAMAGE);
        SetFloat(KNOCKBACK_POWER, knockbackPower);
        
        knockbackTime = getDamageTime;
        SetFloat(KNOCKBACK_TIME, knockbackTime);
    }
}
