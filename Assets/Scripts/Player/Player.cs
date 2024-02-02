using UnityEngine;
using Utility.ScriptableObject;

public class Player : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform playerHandTsf;
    [SerializeField] private ItemObj currentItem;
    [SerializeField] private float pickRange;
    [SerializeField] private float maxHp;
    private float currentHp;

    public void Heal(float healAmount)
    {
        currentHp = Mathf.Min(currentHp + healAmount, maxHp);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) Die();
    }
    
    public Vector2 GetFacing()
    {
        return playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
    }

    private void Pick()
    {
        var colliders = Physics2D.OverlapCircleAll(playerHandTsf.position, pickRange);
        foreach (var collider in colliders)
        {
            var item = collider.GetComponent<ItemObj>();
            if (item == null) continue;
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

    public virtual void Update()
    {
        if (inputController.GetKeyDown("Pick"+playerMovement.playerNumber)) Pick();
        if (inputController.GetKeyDown("Attack"+playerMovement.playerNumber) && currentItem != null) Use();
    }

    public void Die()
    {
        
    }
}
