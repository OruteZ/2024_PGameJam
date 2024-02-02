using UnityEngine;
using Utility.ScriptableObject;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputController inputController;
    
    //movement
    [SerializeField] private PlayerMovement playerMovement;
    
    //hp system
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    
    //heal
    public void Heal(float healAmount)
    {
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void Die()
    {
        
    }
    
    //damage
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public Vector2 GetFacing() => playerMovement.IsFacingRight ? Vector2.right : Vector2.left;

    // 2. 공격
    // 3. 스킬
    // 4. 궁극기
}