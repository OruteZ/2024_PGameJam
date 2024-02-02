using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAdaptor : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rigid;
    
    private static readonly string IS_MOVING = "XMoving";
    private static readonly string IS_ATTACKING = "Attack";
    private static readonly string IS_JUMPING = "IsJumping";
    private static readonly string IS_FALLING = "IsFalling";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetBool(IS_MOVING, Mathf.Abs(_rigid.velocity.x) > 0.5f);
        SetBool(IS_JUMPING, _playerMovement.IsJumping);
        SetBool(IS_FALLING, _rigid.velocity.y < -0.01f);
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
}
